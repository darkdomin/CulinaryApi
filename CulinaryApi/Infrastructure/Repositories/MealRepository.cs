using CulinaryApi.Core.Entieties;
using CulinaryApi.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryApi.Infrastructure.Repositories
{
    public class MealRepository : IMealRepository
    {
        private readonly CulinaryDbContext _dbContext;

        public MealRepository(CulinaryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Meal> GetAsync(int id)
        {
            return await Task.FromResult(_dbContext.Meals.SingleOrDefault(r => r.Id == id));
        }

        public async Task<Meal> GetAsync(string name)
        {
            return await Task.FromResult(_dbContext.Meals.SingleOrDefault(r => r.Name == name));
        }

        public async Task<IEnumerable<Meal>> GetAllAsync(string name = "")
        {
            var meals = _dbContext.Meals.AsEnumerable();

            if (!string.IsNullOrEmpty(name))
            {
                meals = meals
                        .Where(r => r.Name.ToLowerInvariant().Contains(name.ToLowerInvariant()));

            }

            return await Task.FromResult(meals);
        }
        public async Task AddAsync(Meal meal)
        {
            _dbContext.Meals.Add(meal);
            _dbContext.SaveChanges();
            await Task.CompletedTask;
        }

        public Task UpdateAsync()
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Meal meal)
        {
            _dbContext.Meals.Remove(meal);
            _dbContext.SaveChanges();
            await Task.CompletedTask;
        }
    }
}
