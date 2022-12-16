using API.Helpers;
using API.Models;

namespace API.Interfaces
{
    public interface IDiscussionService
    {
        public Task<PagedList<DiscussionModel>> GetDiscussionsAsync(int? gameId, DiscussionParams discussionParams);
        public Task<DiscussionModel> GetDiscussionByIdAsync(int id);
        public Task<PagedList<CommentModel>> GetCommentsAsync(int discussionId, CommentParams commentParams);
        public Task<CommentModel> GetCommentByIdAsync(int id);
        public Task<DiscussionModel> CreateDiscussionAsync(DiscussionModel discussion);
        public Task UpdateDiscussionAsync(DiscussionModel discussion);
        public Task DeleteDiscussionAsync(int id);
        public Task<CommentModel> CreateCommentAsync(CommentModel comment);
        public Task UpdateCommentAsync(CommentModel comment);
        public Task DeleteCommentAsync(int id);
    }
}
