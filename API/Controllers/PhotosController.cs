using API.Data;
using API.Helpers;
using API.Interfaces;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly GameStoreDb _context;

        private readonly IPhotoService _photoService;

        public PhotosController(GameStoreDb context, IPhotoService photoService)
        {
            _context = context;
            _photoService = photoService;
        }

        [Authorize]
        [HttpPost("user/add-photo/{username}")]
        public async Task<ActionResult<PhotoModel>> AddUserPhoto(string username, IFormFile file)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);

            if (user == null)
            {
                return BadRequest("user not found");
            }

            var result = await _photoService.AddPhotoAsync(file, ImageType.Square);

            if (result.Error != null)
            {
                return BadRequest(result.Error.Message);
            }

            if (user.PublicId != null)
            {

                await _photoService.DeletePhotoAsync(user.PublicId);
            }

            var photo = new PhotoModel()
            {
                PhotoUrl = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            return Ok(photo);
        }

        [Authorize]
        [HttpDelete("user/delete-photo/{username}")]
        public async Task<ActionResult<PhotoModel>> DeleteUserPhoto(string username, string publicId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);

            if (user == null)
            {
                return BadRequest("user not found");
            }

            var result = await _photoService.DeletePhotoAsync(publicId);

            if (result.Error != null)
            {
                return BadRequest(result.Error.Message);
            }

            user.PublicId = null;

            user.PhotoUrl = null;

            await _context.SaveChangesAsync();

            return Ok(result);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost("game/add-photo/{id}")]
        public async Task<ActionResult<PhotoModel>> AddGamePhoto(int id, IFormFile file)
        {
            var game = await _context.Games.FirstOrDefaultAsync(x => x.Id == id);

            var result = await _photoService.AddPhotoAsync(file, ImageType.Rectangular);

            if (result.Error != null)
            {
                return BadRequest(result.Error.Message);
            }

            if (game != null && game.PublicId != null)
            {

                await _photoService.DeletePhotoAsync(game.PublicId);
            }

            var photo = new PhotoModel()
            {
                PhotoUrl = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            return Ok(photo);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("game/delete-photo/{id}")]
        public async Task<ActionResult> DeleteGamePhoto(int id, string publicId)
        {
            var game = await _context.Games.FirstOrDefaultAsync(x => x.Id == id);

            var result = await _photoService.DeletePhotoAsync(publicId);

            if (result.Error != null)
            {
                return BadRequest(result.Error.Message);
            }

            game.PublicId = null;
            game.PhotoUrl = null;

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
