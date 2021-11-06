using CulinaryApi.Core.Entieties;
using CulinaryApi.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryApi.Infrastructure.Repositories
{
    public class TimeRepository : ITimeRepository
    {
        private readonly CulinaryDbContext _dbContext;

        public TimeRepository(CulinaryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Time> GetAsync(int id)
        {
            return await Task.FromResult(_dbContext.TImes.SingleOrDefault(r => r.Id == id));
        }

        public async Task<Time> GetAsync(string name)
        {
            return await Task.FromResult(_dbContext.TImes.SingleOrDefault(r => r.Name == name));
        }

        public async Task<IEnumerable<Time>> GetAllAsync(string name = "")
        {
            var times = _dbContext.TImes.AsEnumerable();

            if (!string.IsNullOrEmpty(name))
            {
                times = times
                        .Where(r => r.Name.ToLowerInvariant().Contains(name.ToLowerInvariant()));

            }

            return await Task.FromResult(times);
        }
        public async Task AddAsync(Time time)
        {
            _dbContext.TImes.Add(time);
            _dbContext.SaveChanges();
            await Task.CompletedTask;
        }

        public Task UpdateAsync()
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Time time)
        {
            _dbContext.TImes.Remove(time);
            _dbContext.SaveChanges();
            await Task.CompletedTask;
        }
    }
}
