using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Data.Entities;
using API.Helpers;
using API.Interfaces;
using API.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class GamesService : IGamesService
    {
        private readonly GameStoreDb _dbContext;

        private readonly IMapper _autoMapper;

        public GamesService(GameStoreDb dbContext, IMapper autoMapper)
        {
            _dbContext = dbContext;
            _autoMapper = autoMapper;
        }

        public Task AddAsync(GameModel model)
        {
            return AddInternalAsync(model);
        }

        private async Task AddInternalAsync(GameModel model)
        {
            model.DateAdded = DateTime.Now;
            var entity = _autoMapper.Map<Game>(model);
            var gameCategoriesToAdd = new List<GameCategory>();
            foreach (var c in model.CategoriesId)
            {
                gameCategoriesToAdd.Add(new GameCategory
                {
                    GameId = entity.Id,
                    CategoryId = c,
                });
            }
            entity.Categories = gameCategoriesToAdd;
            await _dbContext.Games.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbContext.Games.SingleOrDefaultAsync(x => x.Id == id);

            if (entity is null)
            {
                throw new ArgumentException(nameof(id));
            }

            _dbContext.Games.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<PagedList<GameModel>> GetAllAsync(GameParams gameParams)
        {
            var query = _dbContext.Games.AsNoTracking().Where(x =>
                x.Downloads >= gameParams.MinDownloads
                && x.Rating >= gameParams.MinRating
                && x.ReleaseDate >= gameParams.MinReleaseDateFilter
                && x.ReleaseDate <= gameParams.MaxReleaseDateFilter
                && x.DateAdded >= gameParams.MinDateAddedFilter
                && x.DateAdded <= gameParams.MaxDateAddedFilter)
                .ProjectTo<GameModel>(_autoMapper.ConfigurationProvider)
                .AsQueryable();

            if (!string.IsNullOrEmpty(gameParams.SearchString) &&
                gameParams.SearchString.Length >= 3)
            {
                query = query.Where(x => x.Name.IndexOf(gameParams.SearchString) != -1);
            }

            if (!string.IsNullOrEmpty(gameParams.PublishersFilter))
            {
                var publishers = gameParams.PublishersFilter.Split(
                    '-', StringSplitOptions.None);
                query = query.Where(x => publishers.Contains(x.Publisher));
            }

            if (!string.IsNullOrEmpty(gameParams.GenresFilter))
            {
                query.Where(x => gameParams.GenresFilter.Split(
                    '-', StringSplitOptions.None).Contains(x.Publisher));
            }

            query = gameParams.SortBy switch
            {
                "Newly Added" => query.OrderByDescending(x => x.DateAdded),
                "Newest Releases" => query.OrderByDescending(x => x.ReleaseDate),
                "Most Popular" => query.OrderByDescending(x => x.Downloads),
                "Best Rated" => query.OrderByDescending(x => x.Rating),
                _ => query.OrderByDescending(x => x.DateAdded)
            };

            if (gameParams.ReverseOrder)
            {
                query = query.Reverse();
            }

            var result = query;

            return await PagedList<GameModel>.CreateAsync(result, (int)gameParams.PageNumber, (int)gameParams.PageSize);
        }

        public async Task<GameModel> GetByIdAsync(int id)
        {
            var result = await _dbContext.Games.Include(g => g.Categories)
                .ThenInclude(gc => gc.Category).Include(g => g.Publisher)
                .SingleOrDefaultAsync(x => x.Id == id);
            if (result is null)
            {
                throw new ArgumentException("Wrong ID");
            }

            return _autoMapper.Map<GameModel>(result);
        }

        public async Task UpdateAsync(GameModel model)
        {
            var entity = await _dbContext.Games.Include(x => x.Categories)
                .SingleOrDefaultAsync(x =>x.Id == model.Id);

            if (entity is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            _autoMapper.Map(model, entity);

            var entityCategoryIds = entity.Categories.Select(c => c.CategoryId);

            var toRemove = entityCategoryIds.Except(model.CategoriesId);

            var toAdd = model.CategoriesId.Except(entityCategoryIds);

            if (toRemove.Any()
                || toAdd.Any())
            {
                var gameCategoriesToAdd = new List<GameCategory>();
                foreach (var c in toAdd)
                {
                    gameCategoriesToAdd.Add(new GameCategory
                    {
                        GameId = entity.Id,
                        CategoryId = c,
                    });
                }

                await _dbContext.GameCategories.AddRangeAsync(gameCategoriesToAdd);

                var gameCategoriesToRemove = _dbContext.GameCategories
                    .Where(x => x.GameId == model.Id)
                    .Where(x => toRemove.Any(y => x.CategoryId == y));

                _dbContext.GameCategories.RemoveRange(gameCategoriesToRemove);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<PublisherModel>> GetPublishersAsync()
        {
            var result = await _dbContext.Publishers
                .ProjectTo<PublisherModel>(_autoMapper.ConfigurationProvider)
                .ToListAsync();

            return result;
        }

        public async Task AddPublisherAsync(string name)
        {
            Publisher publisher = new Publisher
            {
                Name = name,
            };

            await _dbContext.Publishers.AddAsync(publisher);

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdatePublisherAsync(string oldName, string newName)
        {
            var result = await _dbContext.Publishers.SingleOrDefaultAsync(
                x => x.Name.Equals(oldName));

            result.Name = newName;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeletePublisherAsync(string name)
        {
            var result = await _dbContext.Publishers.SingleOrDefaultAsync(
                x => x.Name.Equals(name));

            if (result == null)
            {
                throw new ArgumentException("publisher not found");
            }

            _dbContext.Publishers.Remove(result);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<CategoryModel>> GetCategoriesAsync()
        {
            var result = await _dbContext.Categories
                .ProjectTo<CategoryModel>(_autoMapper.ConfigurationProvider)
                .ToListAsync();

            return result;
        }
    }
}