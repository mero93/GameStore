using API.Data;
using API.Data.Entities;
using API.Helpers;
using API.Interfaces;
using API.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly GameStoreDb _context;
        private readonly IConfiguration _config;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private const int tokenLifeTimeMinutes = 5;

        private const int refreshTokenLifeTimeMonths = 6;

        public AuthController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager,
            GameStoreDb context, IConfiguration config, IMapper mapper
            ,TokenValidationParameters tokenValidationParameters)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _config = config;
            _mapper = mapper;
            _tokenValidationParameters = tokenValidationParameters;
        }

        [HttpPost("register-user")]
        public async Task<ActionResult<AppUserModel>> Register([FromBody] RegisterUserModel registerUserModel)
        {
            if(!ModelState.IsValid ||
                registerUserModel.Password != registerUserModel.ConfirmPassword)
            {
                return BadRequest("Please provide all the required fields");
            }

            var userExists = await _userManager.FindByEmailAsync(registerUserModel.Email);
            if(userExists != null)
            {
                return BadRequest($"Email {registerUserModel.Email} already in use");
            }
            userExists = await _userManager.FindByNameAsync(registerUserModel.UserName);
            if(userExists != null)
            {
                return BadRequest($"Username {registerUserModel.UserName} already in use");
            }
            var user = _mapper.Map<AppUser>(registerUserModel);

            var result = await _userManager.CreateAsync(user, registerUserModel.Password);

            if (!result.Succeeded)
            {
                return BadRequest("AppUser could not be created");
            }

            var roleResult = await _userManager.AddToRoleAsync(user, "Member");
            if (!roleResult.Succeeded)
            {
                return BadRequest("Could not add role");  
            }

            var jwtToken = await GenerateJwtTokenAsync(user, null);

            var userModel = new AppUserModel
            {
                Username = registerUserModel.UserName,
                Token = jwtToken.Token,
                RefreshToken = jwtToken.RefreshToken
            };

            return userModel;
        }

        [HttpPost("login-user")]
        public async Task<ActionResult<AppUserModel>> Login([FromBody] LoginUserModel loginUserModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please, provide all required fields");
            }

            AppUser user;
            if (loginUserModel.UserNameOrEmail.IsValidUsername())
            {
                user = await _userManager.FindByNameAsync(loginUserModel.UserNameOrEmail);
            }
            else
            {
                user = await _userManager.FindByEmailAsync(loginUserModel.UserNameOrEmail);
            }
            if (user == null)
            {
                return Unauthorized("Invalid username or email");
            }

            var refreshToken = (await _context.RefreshTokens.SingleOrDefaultAsync(x =>
                x.UserId == user.Id));

            var tokenValue = await GenerateJwtTokenAsync(user, refreshToken);

            var roles = await _userManager.GetRolesAsync(user);

            var userModel = new AppUserModel
            {
                Username = user.UserName,
                Token = tokenValue.Token,
                RefreshToken = tokenValue.RefreshToken,
                Roles = roles,
                PhotoUrl = user.PhotoUrl,
                ExpiresAt = DateTime.Now.AddMinutes(tokenLifeTimeMinutes),
            };

            return Ok(userModel);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<AuthorizationModel>> RefreshToken([FromBody]AuthorizationModel authorizationModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Something went wrong with authorization");
            }

            var result = await VerifyAndGenerateTokenAsync(authorizationModel);

            return Ok(result);
        }

        private async Task<AuthorizationModel> VerifyAndGenerateTokenAsync(AuthorizationModel authorizationModel)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var storedToken = await _context.RefreshTokens.FirstOrDefaultAsync(x =>
            x.Token == authorizationModel.RefreshToken);
            var appUser = await _userManager.Users.SingleOrDefaultAsync(x => 
            x.Id == storedToken.UserId);

            try
            {
                _ = jwtTokenHandler.ValidateToken(authorizationModel.Token,
                    _tokenValidationParameters, out var validatedToken);

                return await GenerateJwtTokenAsync(appUser, storedToken);
            }
            catch (SecurityTokenExpiredException)
            {
                if(storedToken.DateExpire >= DateTime.Now)
                {
                    return await GenerateJwtTokenAsync(appUser, storedToken);
                } else
                {
                    return await GenerateJwtTokenAsync(appUser, null);
                }
            }
        }

        private async Task<AuthorizationModel> GenerateJwtTokenAsync(AppUser appUser, RefreshToken storedToken)
        {
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, appUser.UserName),
                new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, appUser.Email),
                new Claim(JwtRegisteredClaimNames.Sub, appUser.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            //Add User Role Claims
            var userRoles = await _userManager.GetRolesAsync(appUser);

            authClaims.AddRange(userRoles.Select(x => new Claim(ClaimTypes.Role, x)));

            var authSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["JWT:Secret"]));

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddMinutes(tokenLifeTimeMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            if(storedToken != null)
            {
                var response = new AuthorizationModel()
                {
                    RefreshToken = storedToken.Token,
                    ExpiresAt = token.ValidTo,
                    Token = jwtToken,
                };

                return response;
            }

            else
            {
                var refreshToken = new RefreshToken()

                {
                    JwtId = token.Id,
                    IsRevoked = false,
                    UserId = appUser.Id,
                    DateAdded = DateTime.Now,
                    DateExpire = DateTime.Now.AddMonths(refreshTokenLifeTimeMonths),
                    Token = Guid.NewGuid().ToString() + "-" + Guid.NewGuid().ToString(),
                };
                await _context.RefreshTokens.AddAsync(refreshToken);
                await _context.SaveChangesAsync();

                var response = new AuthorizationModel()
                {
                    RefreshToken = refreshToken.Token,
                    ExpiresAt = token.ValidTo,
                    Token = jwtToken,
                };

                return response;
            }
        }

        [HttpPut("add-info/{id}")]
        public async Task<ActionResult> AddInfo(int id, AdditionalInfoModel info)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == id);

            user.FirstName = info.FirstName;

            user.LastName = info.LastName;

            user.Phone = info.Phone;

            user.PaymentType = info.PaymentType;

            user.OrderComment = info.OrderComment;

            user.AdditionalInfo = true;

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("get-info/{id}")]
        public async Task<ActionResult<AdditionalInfoModel>> GetInfo(int id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            var info = _mapper.Map<AdditionalInfoModel>(user);

            return Ok(info);
        }
    }
}
