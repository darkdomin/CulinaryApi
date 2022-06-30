using CulinaryApi.Core.Entieties;
using CulinaryApi.Core.Repositories;
using CulinaryApi.Infrastructure.DTO.Recipes;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IQueryable<Recipe>> GetAll(int? userId)
        {
            var result = await Task.FromResult(_dbContext
                                        .Recipes
                                        .Where(u=>u.CreateById == userId)
                                        .Include(m => m.Meal)
                                        .Include(c => c.Cuisine)
                                        .Include(t => t.Time)
                                        .Include(d => d.Difficult));
            return result;
                                          
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

        public async Task<IQueryable<Recipe>> GetAllAsync(int? userId, RecipeQuery query)
        {
            var baseRecipe = _dbContext
                             .Recipes
                             .Where(r => r.CreateById == userId &&
                                         (query.SearchPhrase == null ||
                                           r.Name.ToLower().Contains(query.SearchPhrase.ToLower())
                                         )
                                         && (query.Meal == 0 || r.MealId == query.Meal)
                                         && (query.Cuisine == 0 || r.CuisineId == query.Cuisine)
                                         && (query.Level == 0 || r.DifficultId == query.Level)
                                         && (query.Time == 0 || r.TimeId == query.Time)
                                    )
                          .Include(m => m.Meal)
                          .Include(c => c.Cuisine)
                          .Include(t => t.Time)
                          .Include(d => d.Difficult);

            return await Task.FromResult(baseRecipe);
        }

        public async Task<IQueryable<Recipe>> GetFilterAsync(int? userId, RecipeQuery query)
        {
            var baseRecipe = _dbContext
                             .Recipes
                             .Where(r => r.CreateById == userId &&
                                         (query.SearchPhrase == null ||
                                           r.Name.ToLower().Contains(query.SearchPhrase.ToLower())
                                         ) 
                                         && query.Meal == 0 || r.MealId == query.Meal
                                         && query.Cuisine == 0 || r.CuisineId == query.Cuisine
                                         && query.Level == 0 || r.DifficultId == query.Level
                                         && query.Time == 0 || r.TimeId == query.Time  
                                    )
                          .Include(m => m.Meal)
                          .Include(c => c.Cuisine)
                          .Include(t => t.Time)
                          .Include(d => d.Difficult);

            return await Task.FromResult(baseRecipe);
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
