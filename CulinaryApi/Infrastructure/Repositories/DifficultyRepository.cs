using CulinaryApi.Core.Entieties;
using CulinaryApi.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryApi.Infrastructure.Repositories
{
    public class DifficultyRepository : IDifficultyRepository
    {
        private readonly CulinaryDbContext _dbContext;

        public DifficultyRepository(CulinaryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Difficulty> GetAsync(int id)
        {
            return await Task.FromResult(_dbContext.Difficulties.SingleOrDefault(r => r.Id == id));
        }

        public async Task<Difficulty> GetAsync(string name)
        {
            return await Task.FromResult(_dbContext.Difficulties.SingleOrDefault(r => r.Name == name));
        }

        public async Task<IEnumerable<Difficulty>> GetAllAsync(string name = "")
        {
            var difficulties = _dbContext.Difficulties.AsEnumerable();

            if (!string.IsNullOrEmpty(name))
            {
                difficulties = difficulties
                               .Where(r => r.Name.ToLowerInvariant().Contains(name.ToLowerInvariant()));

            }

            return await Task.FromResult(difficulties);
        }
        public async Task AddAsync(Difficulty difficulty)
        {
            _dbContext.Difficulties.Add(difficulty);
            _dbContext.SaveChanges();
            await Task.CompletedTask;
        }

        public Task UpdateAsync()
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Difficulty difficulty)
        {
            _dbContext.Difficulties.Remove(difficulty);
            _dbContext.SaveChanges();
            await Task.CompletedTask;
        }
    }
}
