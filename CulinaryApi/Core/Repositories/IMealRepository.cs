using CulinaryApi.Core.Entieties;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CulinaryApi.Core.Repositories
{
    public interface IMealRepository
    {
        Task<Meal> GetAsync(int id);
        Task<Meal> GetAsync(string name);
        Task<IEnumerable<Meal>> GetAllAsync(string name = "");
        Task AddAsync(Meal meal);
        Task UpdateAsync();
        Task DeleteAsync(Meal meal);
    }
}
