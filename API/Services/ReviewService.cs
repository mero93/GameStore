using API.Data;
using API.Data.Entities;
using API.Helpers;
using API.Interfaces;
using API.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class ReviewService : IReviewService
    {
        private readonly GameStoreDb _dbContext;

        private readonly IMapper _autoMapper;

        public ReviewService(GameStoreDb dbContext, IMapper autoMapper)
        {
            _dbContext = dbContext;
            _autoMapper = autoMapper;
        }

        public async Task CreateReviewAsync(ReviewModel review)
        {
            var game = await _dbContext.Games.Include(x => x.Reviews)
                .SingleOrDefaultAsync(x => x.Id == review.GameId);
            game.RatingNumber += 1;
            game.Rating = (game.Reviews.Sum(x => x.Rating) + review.Rating)
                / game.RatingNumber;
            await _dbContext.Reviews.AddAsync(_autoMapper.Map<Review>(review));
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteReviewAsync(int gameId, int appUserId)
        {
            var game = await _dbContext.Games.Include(x => x.Reviews)
                .SingleOrDefaultAsync(x => x.Id == gameId);
            if (game == null)
            {
                throw new ArgumentException("Wrong game Id");
            }
            if (!game.Reviews.Any(x => x.AppUserId == appUserId))
            {
                throw new ArgumentException("Wrong user Id");
            }

            game.Reviews = game.Reviews.Where(x => x.AppUserId != appUserId).ToList();

            game.RatingNumber -= 1;

            game.Rating = game.Reviews.Average(x => x.Rating);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ReviewModel>> GetGameReviewsAsync(int gameId, ReviewParams reviewParams)
        {
            var result = _dbContext.Reviews
                .ProjectTo<ReviewModel>(_autoMapper.ConfigurationProvider)
                .Where(x => x.GameId == gameId);

            return await PagedList<ReviewModel>
                .CreateAsync(result, (int)reviewParams.PageNumber, (int)reviewParams.PageSize);
        }

        public async Task<IEnumerable<ReviewModel>> GetUserReviewsAsync(int appUserId, ReviewParams reviewParams)
        {
            var result = _dbContext.Reviews
                .ProjectTo<ReviewModel>(_autoMapper.ConfigurationProvider)
                .Where(x => x.AppUserId == appUserId);

            return await PagedList<ReviewModel>
                .CreateAsync(result, (int)reviewParams.PageNumber, (int)reviewParams.PageSize);
        }

        public async Task UpdateReviewAsync(ReviewModel review)
        {
            var game = await _dbContext.Games.Include(x => x.Reviews)
                .SingleOrDefaultAsync(x => x.Id == review.GameId);
            if (game == null)
            {
                throw new ArgumentException("Wrong game Id");
            }
            var entity = game.Reviews.SingleOrDefault(
                x => x.AppUserId == review.AppUserId);
            if (entity == null)
            {
                throw new ArgumentException("Wrong user Id");
            }
            _autoMapper.Map(review, entity);
            game.Rating = game.Reviews.Average(x => x.Rating);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ReviewModel> GetById(int gameId, int appUserId)
        {
            var result = await _dbContext.Reviews
                .ProjectTo<ReviewModel>(_autoMapper.ConfigurationProvider)
                .SingleOrDefaultAsync(x => x.GameId == gameId && x.AppUserId == appUserId);

            if (result is null)
            {
                throw new ArgumentException("Wrong ID");
            }

            return result;
        }
    }
}
