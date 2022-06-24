using System.Collections.Generic;
using System.Threading.Tasks;

namespace CulinaryApi.Core.Repositories
{
    public interface IFilterRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetAsync(int id);
        Task<TEntity> GetAsync(string name);
        Task<IEnumerable<TEntity>> GetAllAsync(string name = "");
        Task AddAsync(TEntity filter);
        Task UpdateAsync();
        Task DeleteAsync(TEntity filter);
    }
}
