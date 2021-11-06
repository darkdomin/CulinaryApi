using CulinaryApi.Core.Entieties;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CulinaryApi.Core.Repositories
{
    public interface IDifficultyRepository
    {
        Task<Difficulty> GetAsync(int id);
        Task<Difficulty> GetAsync(string name);
        Task<IEnumerable<Difficulty>> GetAllAsync(string name = "");
        Task AddAsync(Difficulty difficulty);
        Task UpdateAsync();
        Task DeleteAsync(Difficulty difficulty);
    }
}
