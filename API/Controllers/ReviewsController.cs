using API.Helpers;
using API.Interfaces;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet("game-reviews/{gameId}")]
        public async Task<ActionResult<IEnumerable<ReviewModel>>> GetGameReviewsAsync(
            int gameId, [FromQuery] ReviewParams reviewParams)
        {
            if (reviewParams == null)
            {
                reviewParams = new ReviewParams();
            }

            var result = await _reviewService.GetGameReviewsAsync(gameId, reviewParams);

            return Ok(result);
        }

        [HttpGet("user-reviews/{appUserId}")]
        public async Task<ActionResult<IEnumerable<ReviewModel>>> GetUserReviewsAsync(
            int appUserId, [FromQuery] ReviewParams reviewParams)
        {
            if (reviewParams == null)
            {
                reviewParams = new ReviewParams();
            }

            var result = await _reviewService.GetUserReviewsAsync(appUserId, reviewParams);

            return Ok(result);
        }

        [HttpGet("game/{gameId}/user/{appUserId}")]
        public async Task<ActionResult<ReviewModel>> GetReviewAsync(int gameId, int appUserId)
        {
            var result = await _reviewService.GetById(gameId, appUserId);

            return Ok(result);
        }

        [HttpPost("create-review")]
        public async Task<ActionResult> CreateReviewAsync(ReviewModel review)
        {
            if (review == null || !ModelState.IsValid)
            {
                return BadRequest("Please provide all the required fields");
            }

            await _reviewService.CreateReviewAsync(review);

            return Ok("Review created successfully");
        }

        [HttpPut("update-review")]
        public async Task<ActionResult> UpdateReviewAsync(ReviewModel review)
        {
            if (review == null || !ModelState.IsValid)
            {
                return BadRequest("Please provide all the required fields");
            }

            await _reviewService.UpdateReviewAsync(review);

            return Ok("Review updated successfully");
        }

        [HttpDelete("delete-discussion/game/{gameId}/user/{appUserId}")]
        public async Task<ActionResult> DeleteReviewAsync(int gameId, int appUserId)
        {
            await _reviewService.DeleteReviewAsync(gameId, appUserId);

            return Ok("Review deleted successfully");
        }
    }
}
