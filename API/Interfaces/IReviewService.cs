using API.Helpers;
using API.Models;

namespace API.Interfaces
{
    public interface IReviewService
    {
        public Task<IEnumerable<ReviewModel>> GetGameReviewsAsync(int gameId, ReviewParams reviewParams);
        public Task<IEnumerable<ReviewModel>> GetUserReviewsAsync(int appUserId, ReviewParams reviewParams);
        public Task<ReviewModel> GetById(int gameId, int appUserId);
        public Task CreateReviewAsync(ReviewModel review);
        public Task UpdateReviewAsync(ReviewModel review);
        public Task DeleteReviewAsync(int gameId, int appUserId);
    }
}