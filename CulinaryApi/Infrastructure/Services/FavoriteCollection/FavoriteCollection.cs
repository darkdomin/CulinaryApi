using CulinaryApi.Core.Entieties;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryApi.Infrastructure.Services.FavoriteCollection
{
    public class FavoriteCollection : IFavoriteCollection
    {
        private readonly CulinaryDbContext _dbContext;

        public FavoriteCollection(CulinaryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Add(FavoriteRecipe favoriteRecipe)
        {
            _dbContext.FavoriteRecipe.Add(favoriteRecipe);
            _dbContext.SaveChanges();
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<FavoriteRecipe>> GetAll()
        {
            return await Task.FromResult(_dbContext.FavoriteRecipe);
        }

        public async Task<FavoriteRecipe> GetId(int recipeId)
        {
            var result = _dbContext.FavoriteRecipe.FirstOrDefault(r => r.RecipeId == recipeId);
            return await Task.FromResult(result);
        }

        public async Task Remove(FavoriteRecipe favoriteRecipe)
        {
            _dbContext.FavoriteRecipe.Remove(favoriteRecipe);
            _dbContext.SaveChanges();
            await Task.CompletedTask;
        }

        public async Task Update(FavoriteRecipe favoriteRecipe)
        {
            _dbContext.FavoriteRecipe.Update(favoriteRecipe);
            _dbContext.SaveChanges();
            await Task.CompletedTask;
        }
        public async Task<IEnumerable<FavoriteRecipe>> GetFavorites()
        {
            var all = await GetAll();
            var result = all.Where(r => r.Counter > 0)
                              .OrderByDescending(r => r.Counter)
                              .Take(6);
            return await Task.FromResult(result);
        }

        public async Task DeleteAll()
        {
            var all = await GetAll();
            _dbContext.FavoriteRecipe.RemoveRange(all);
            _dbContext.SaveChanges();
            await Task.CompletedTask;
        }
    }
}
