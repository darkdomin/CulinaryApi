using CulinaryApi.Core.Entieties;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CulinaryApi.Core.Repositories
{
    public interface ITimeRepository
    {
        Task<Time> GetAsync(int id);
        Task<Time> GetAsync(string name);
        Task<IEnumerable<Time>> GetAllAsync(string name = "");
        Task AddAsync(Time time);
        Task UpdateAsync();
        Task DeleteAsync(Time time);
    }
}
