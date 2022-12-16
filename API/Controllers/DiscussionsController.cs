using API.Data.Entities;
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
    public class DiscussionsController : ControllerBase
    {
        private readonly IDiscussionService _discussionService;

        public DiscussionsController(IDiscussionService discussionService)
        {
            _discussionService = discussionService;
        }

        [HttpGet("{gameId?}")]
        public async Task<ActionResult<IEnumerable<DiscussionModel>>> GetDiscussionsAsync(
            int? gameId, [FromQuery] DiscussionParams discussionParams)
        {
            if (discussionParams == null)
            {
                discussionParams = new DiscussionParams();
            }

            var result = await _discussionService.GetDiscussionsAsync(gameId, discussionParams);


            return Ok(result);
        }

        [HttpGet("details/{id}")]
        public async Task<ActionResult<DiscussionModel>> GetDiscussionByIdAsync(int id)
        {
            var result = await _discussionService.GetDiscussionByIdAsync(id);

            return Ok(result);
        }

        [HttpGet("comments/{discussionId}")]
        public async Task<ActionResult<IEnumerable<CommentModel>>> GetCommentsAsync(
            int discussionId, [FromQuery] CommentParams commentParams)
        {
            if (commentParams == null)
            {
                commentParams = new CommentParams();
            }

            var result = await _discussionService.GetCommentsAsync(discussionId, commentParams);
            Response.AddPaginationHeader(result.CurrentPage, result.PageSize, result.TotalCount,
                result.TotalPages);

            return Ok(result);
        }

        [HttpPost("create-discussion")]
        public async Task<ActionResult<DiscussionModel>> CreateDiscussion(DiscussionModel discussion)
        {
            if (discussion == null || !ModelState.IsValid)
            {
                return BadRequest("Please provide all the required fields");
            }

            var result = await _discussionService.CreateDiscussionAsync(discussion);

            return Ok(result);
        }

        [HttpPost("create-comment")]
        public async Task<ActionResult<CommentModel>> CreateComment(CommentModel comment)
        {
            if (comment == null || !ModelState.IsValid)
            {
                return BadRequest("Please provide all the required fields");
            }

            var result = await _discussionService.CreateCommentAsync(comment);

            return Ok(result);
        }

        [HttpPut("update-discussion")]
        public async Task<ActionResult<DiscussionModel>> UpdateDiscussion(DiscussionModel discussion)
        {
            if (discussion == null || !ModelState.IsValid)
            {
                return BadRequest("Please provide all the required fields");
            }

            await _discussionService.UpdateDiscussionAsync(discussion);

            return Ok(discussion);
        }

        [HttpPut("update-comment")]
        public async Task<ActionResult<CommentModel>> UpdateComment(CommentModel comment)
        {
            if (comment == null || !ModelState.IsValid)
            {
                return BadRequest("Please provide all the required fields");
            }

            await _discussionService.UpdateCommentAsync(comment);

            return Ok(comment);
        }

        [HttpDelete("delete-discussion/{id}")]
        public async Task<ActionResult> DeleteDiscussion(int id)
        {
            await _discussionService.DeleteDiscussionAsync(id);

            return Ok("Discussion deleted successfully");
        }

        [HttpDelete("delete-comment/{id}")]
        public async Task<ActionResult> DeleteComment(int id)
        {
            await _discussionService.DeleteCommentAsync(id);

            return Ok();
        }
    }
}
