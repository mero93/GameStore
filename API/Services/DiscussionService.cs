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
    public class DiscussionService : IDiscussionService
    {
        private readonly GameStoreDb _dbContext;

        private readonly IMapper _autoMapper;

        public DiscussionService(GameStoreDb dbContext, IMapper autoMapper)
        {
            _dbContext = dbContext;
            _autoMapper = autoMapper;
        }

        public async Task<CommentModel> CreateCommentAsync(CommentModel comment)
        {
            if (await _dbContext.Users.FindAsync(comment.AppUserId) == null)
            {
                throw new ArgumentException("Wrong User Id");
            }
            if (await _dbContext.Games.FindAsync(comment.DiscussionId) == null)
            {
                throw new ArgumentException("Wrong Discussion Id");
            }
            comment.DateCreated = DateTime.Now;
            var entity = _autoMapper.Map<Comment>(comment);
            await _dbContext.Comments.AddAsync(entity);
            var discussion = await _dbContext.Discussions
                .FindAsync(comment.DiscussionId);
            discussion.LastActivity = DateTime.Now;
            await _dbContext.SaveChangesAsync();

            return _autoMapper.Map<CommentModel>(entity);
        }

        public async Task<DiscussionModel> CreateDiscussionAsync(DiscussionModel discussion)
        {
            if (await _dbContext.Users.FindAsync(discussion.AppUserId) == null)
            {
                throw new ArgumentException("Wrong User Id");
            }
            if (await _dbContext.Games.FindAsync(discussion.GameId) == null)
            {
                throw new ArgumentException("Wrong Game Id");
            }
            
            discussion.DateCreated = DateTime.Now;
            discussion.LastActivity = DateTime.Now;
            var entity = _autoMapper.Map<Discussion>(discussion);
            await _dbContext.Discussions.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return _autoMapper.Map<DiscussionModel>(entity);
        }

        public Task DeleteCommentAsync(int id)
        {
            var comment = _dbContext.Comments.SingleOrDefault(c => c.Id == id);
            if (comment == null)
            {
                throw new ArgumentException(nameof(id));
            }

            return InnerDeleteCommentAsync(comment);
        }

        private async Task InnerDeleteCommentAsync(Comment comment)
        {
            _dbContext.Comments.Remove(comment);
            await _dbContext.SaveChangesAsync();
        }

        public Task DeleteDiscussionAsync(int id)
        {
            var discussion = _dbContext.Discussions.SingleOrDefault(c => c.Id == id);
            if (discussion == null)
            {
                throw new ArgumentException(nameof(id));
            }

            return InnerDeleteDiscussionAsync(discussion);
        }

        private async Task InnerDeleteDiscussionAsync(Discussion discussion)
        {
            _dbContext.Discussions.Remove(discussion);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<CommentModel> GetCommentByIdAsync(int id)
        {
            var result = await _dbContext.Comments
                .ProjectTo<CommentModel>(_autoMapper.ConfigurationProvider)
                .SingleOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<PagedList<CommentModel>> GetCommentsAsync(int discussionId, CommentParams commentParams)
        {
            var query = await _dbContext.Comments
                .ProjectTo<CommentModel>(_autoMapper.ConfigurationProvider)
                .Where(x => x.DiscussionId == discussionId).ToArrayAsync();

            var result = query.Where(x => !x.CommentId.HasValue)
                .GroupJoin(query.Where(x => x.CommentId.HasValue),
                comment => comment.Id, reply => reply.CommentId, (comment, reply) =>
                {
                    comment.Replies = reply;
                    return comment;
                });

            var count = result.Count();

            return PagedList<CommentModel>
                .Create(result, (int)commentParams.PageNumber, (int)commentParams.PageSize, count);
        }

        public async Task<DiscussionModel> GetDiscussionByIdAsync(int id)
        {
            var result = await _dbContext.Discussions
                .ProjectTo<DiscussionModel>(_autoMapper.ConfigurationProvider)
                .SingleOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<PagedList<DiscussionModel>> GetDiscussionsAsync(int? gameId, DiscussionParams discussionParams)
        {
            IQueryable<DiscussionModel> result;

            if (gameId == null)
            {
                result = _dbContext.Discussions
                    .ProjectTo<DiscussionModel>(_autoMapper.ConfigurationProvider)
                    .OrderByDescending(x => x.DateCreated);
            }

            else
            {
                result = _dbContext.Discussions
                    .ProjectTo<DiscussionModel>(_autoMapper.ConfigurationProvider)
                    .OrderByDescending(x => x.DateCreated)
                    .Where(x => x.GameId == gameId);
            }

            return await PagedList<DiscussionModel>
                .CreateAsync(result, (int)discussionParams.PageNumber, (int)discussionParams.PageSize);
        }

        public Task UpdateCommentAsync(CommentModel comment)
        {
            comment.DateUpdated = DateTime.Now;
            var entity = _dbContext.Comments.SingleOrDefault(c => c.Id == comment.Id);
            if (entity == null)
            {
                throw new ArgumentException(nameof(comment));
            }

            return InnerUpdateCommentAsync(entity, comment);
        }

        private async Task InnerUpdateCommentAsync(Comment entity, CommentModel model)
        {
            _autoMapper.Map(model, entity);
            await _dbContext.SaveChangesAsync();
        }

        public Task UpdateDiscussionAsync(DiscussionModel discussion)
        {
            discussion.DateUpdated = DateTime.Now;
            discussion.LastActivity = DateTime.Now;
            var entity = _dbContext.Discussions.SingleOrDefault(c => c.Id == discussion.Id);
            if (entity == null)
            {
                throw new ArgumentException(nameof(discussion));
            }

            return InnerUpdateDiscussionAsync(entity, discussion);
        }

        private async Task InnerUpdateDiscussionAsync(Discussion entity, DiscussionModel model)
        {
            _autoMapper.Map(model, entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
