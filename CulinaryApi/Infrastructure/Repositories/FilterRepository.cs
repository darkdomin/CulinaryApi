using CulinaryApi.Core.Entieties;
using CulinaryApi.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryApi.Infrastructure.Repositories
{
    public class FilterRepository<TEntity> : IFilterRepository<TEntity> where TEntity : Filter<TEntity>
    {
        private readonly CulinaryDbContext _dbContext;

        public FilterRepository(CulinaryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await Task.FromResult(_dbContext.Set<TEntity>().AsNoTracking().SingleOrDefault(r => r.Id == id));
        }

        public async Task<TEntity> GetAsync(string name)
        {
            return await Task.FromResult(_dbContext.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefault(r => r.Name == name));
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(string name = "")
        {
            var meals = _dbContext.Set<TEntity>().AsNoTracking().AsEnumerable();

            if (!string.IsNullOrEmpty(name))
            {
                meals = meals
                        .Where(r => r.Name.ToLowerInvariant().Contains(name.ToLowerInvariant()));

            }

            return await Task.FromResult(meals);
        }
        public async Task AddAsync(TEntity filter)
        {
            _dbContext.Set<TEntity>().Add(filter);
            _dbContext.SaveChanges();
            await Task.CompletedTask;
        }

        public async Task UpdateAsync()
        {
            _dbContext.SaveChanges();
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(TEntity filter)
        {
            _dbContext.Set<TEntity>().Remove(filter);
            _dbContext.SaveChanges();
            await Task.CompletedTask;
        }
    }
}
