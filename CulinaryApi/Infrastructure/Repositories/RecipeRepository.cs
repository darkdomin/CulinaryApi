using CulinaryApi.Core.Entieties;
using CulinaryApi.Core.Repositories;
using Microsoft.EntityFrameworkCore;
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
        }                                                                  

        public async Task<Recipe> GetAsync(int id)
        {
            var result = await Task.FromResult(_dbContext
                                         .Recipes
                                         .Include(m => m.Meal)
                                         .Include(c => c.Cuisine)
                                         .Include(t => t.Time)
                                         .Include(d => d.Difficult)
                                         .SingleOrDefault(r => r.Id == id));
            return result;
        }

        public async Task<Recipe> GetAsync(string name)
        {
            return await Task.FromResult(_dbContext
                                         .Recipes
                                         .Include(m => m.Meal)
                                         .Include(c => c.Cuisine)
                                         .Include(t => t.Time)
                                         .Include(d => d.Difficult)
                                         .SingleOrDefault(r => r.Name == name));
                                         
        }

        public async Task<IEnumerable<Recipe>> GetAllAsync(string name = "")
        {
            var recipes = _dbContext
                          .Recipes
                          .Include(m=>m.Meal)
                          .Include(c=>c.Cuisine)
                          .Include(t=>t.Time)
                          .Include(d=>d.Difficult)
                          .AsEnumerable();

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
