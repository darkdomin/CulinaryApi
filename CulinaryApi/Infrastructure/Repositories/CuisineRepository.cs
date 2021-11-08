using CulinaryApi.Core.Entieties;
using CulinaryApi.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryApi.Infrastructure.Repositories
{
    public class CuisineRepository : ICuisineRepository
    {
        private readonly CulinaryDbContext _dbContext;

        public CuisineRepository(CulinaryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Cuisine> GetAsync(int id)
        {
            return await Task.FromResult(_dbContext.Cuisines.SingleOrDefault(r => r.Id == id));
        }

        public async Task<Cuisine> GetAsync(string name)
        {
            return await Task.FromResult(_dbContext.Cuisines.SingleOrDefault(r => r.Name == name));
        }

        public async Task<IEnumerable<Cuisine>> GetAllAsync(string name = "")
        {
            var cuisines = _dbContext.Cuisines.AsEnumerable();
           
            if (!string.IsNullOrEmpty(name))
            {
                cuisines = cuisines
                           .Where(r => r.Name.ToLowerInvariant().Contains(name.ToLowerInvariant()));

            }

            return await Task.FromResult(cuisines);
        }
        public async Task AddAsync(Cuisine cuisine)
        {
            _dbContext.Cuisines.Add(cuisine);
            _dbContext.SaveChanges();
            await Task.CompletedTask;
        }

        public async Task UpdateAsync()
        {
            _dbContext.SaveChanges();
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Cuisine cuisine)
        {
            _dbContext.Cuisines.Remove(cuisine);
            _dbContext.SaveChanges();
            await Task.CompletedTask;
        }
    }
}
