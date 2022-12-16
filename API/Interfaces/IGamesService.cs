using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Helpers;
using API.Models;

namespace API.Interfaces
{
    public interface IGamesService
    {
        Task<PagedList<GameModel>> GetAllAsync(GameParams gameParams);

        Task<GameModel> GetByIdAsync(int id);

        Task AddAsync(GameModel model);

        Task UpdateAsync(GameModel model);

        Task DeleteAsync(int id);

        Task DeletePublisherAsync(string name);

        Task UpdatePublisherAsync(string oldName, string newName);

        Task AddPublisherAsync(string name);

        Task<IEnumerable<PublisherModel>> GetPublishersAsync();

        Task<IEnumerable<CategoryModel>> GetCategoriesAsync();
    }
}