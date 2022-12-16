using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.Entities;
using API.Helpers;
using API.Interfaces;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGamesService _gamesService;

        private readonly IPhotoService _photoService;

        public GamesController(IGamesService gamesService, IPhotoService photoService)
        {
            _gamesService = gamesService;
            _photoService = photoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameModel>>> Get([FromQuery]GameParams gameParams)
        {
             if (gameParams == null)
            {
                gameParams = new GameParams();
            }

            var result = await _gamesService.GetAllAsync(gameParams);
            Response.AddPaginationHeader(result.CurrentPage, result.PageSize, result.TotalCount,
                result.TotalPages);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameModel>> GetGame(int id)
        {
            var result = await _gamesService.GetByIdAsync(id);

            return Ok(result);
        }


        [HttpGet("get-publishers")]
        public async Task<ActionResult<IEnumerable<PublisherModel>>> GetPublishers()
        {
            var result = await _gamesService.GetPublishersAsync();

            return Ok(result);
        }

        [HttpGet("get-genres")]
        public async Task<ActionResult<IEnumerable<CategoryModel>>> GetCategoriesAsync()
        {
            var result = await _gamesService.GetCategoriesAsync();

            return Ok(result);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost("create-game")]
        public async Task<ActionResult<GameModel>> CreateGame(GameModel newGame)
        {
            if (newGame == null || !ModelState.IsValid)
            {
                return BadRequest("Please provide all the required fields");
            }

            await _gamesService.AddAsync(newGame);

            return Ok();
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPut("update-game")]
        public async Task<ActionResult> UpdateGame(GameModel newGame)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please provide all the required fields");
            }

            await _gamesService.UpdateAsync(newGame);

            return Ok();
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("remove-game/{id}")]
        public async Task<ActionResult> RemoveGame(int id)
        {
            try
            {
                await _gamesService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("Game removed successfully");
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost("add-photo/{id}")]
        public async Task<ActionResult<PhotoModel>> AddPhoto(int id, IFormFile file)
        {
            var game = await _gamesService.GetByIdAsync(id);

            var result = await _photoService.AddPhotoAsync(file, ImageType.Rectangular);

            if (result.Error != null)
            {
                return BadRequest(result.Error.Message);
            }

            var photo = new PhotoModel()
            {
                PhotoUrl = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };
            game.PublicId = photo.PublicId;

            game.PhotoUrl = photo.PhotoUrl;

            await _gamesService.UpdateAsync(game);

            return Ok(photo);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("delete-photo/{id}")]
        public async Task<ActionResult<PhotoModel>> DeletePhoto(int id, string publicId)
        {
            var game = await _gamesService.GetByIdAsync(id);

            var result = await _photoService.DeletePhotoAsync(publicId);

            if (result.Error != null)
            {
                return BadRequest(result.Error.Message);
            }

            game.PublicId = null;
            game.PhotoUrl = null;

            await _gamesService.UpdateAsync(game);

            return Ok(result);
        }
    }
}