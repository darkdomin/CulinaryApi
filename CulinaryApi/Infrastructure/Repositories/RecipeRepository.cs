using CulinaryApi.Core.Entieties;
using CulinaryApi.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryApi.Infrastructure.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly CulinaryDbContext _dbContext;

        public RecipeRepository(CulinaryDbContext dbContext)
        {
            _dbContext = dbContext;
        }                                                                     //dodac wyjatki itd

        public async Task<Recipe> GetAsync(int id)
        {
            return await Task.FromResult(_dbContext.Recipes.SingleOrDefault(r => r.Id == id));
        }

        public async Task<Recipe> GetAsync(string name)
        {
            return await Task.FromResult(_dbContext.Recipes.SingleOrDefault(r => r.Name == name));
        }

        public async Task<IEnumerable<Recipe>> GetAllAsync(string name = "")
        {
            var recipes = _dbContext.Recipes.AsEnumerable();

            if (!string.IsNullOrEmpty(name))
            {
                recipes = recipes
                          .Where(r => r.Name.ToLowerInvariant().Contains(name.ToLowerInvariant()));

            }

            return await Task.FromResult(recipes);
        }

        public async Task AddAsync(Recipe recipe)
        {
            _dbContext.Recipes.Add(recipe);
            _dbContext.SaveChanges();
            await Task.CompletedTask;
        }

        public async Task UpdateAsync()
        {
            _dbContext.SaveChanges();
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Recipe recipe)
        {
            _dbContext.Recipes.Remove(recipe);
            _dbContext.SaveChanges();
            await Task.CompletedTask;
        }
    }
}
