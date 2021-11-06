using CulinaryApi.Core.Entieties;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CulinaryApi.Core.Repositories
{
    public interface ICuisineRepository
    {
        Task<Cuisine> GetAsync(int id);
        Task<Cuisine> GetAsync(string name);
        Task<IEnumerable<Cuisine>> GetAllAsync(string name = "");
        Task AddAsync(Cuisine cuisine);
        Task UpdateAsync();
        Task DeleteAsync(Cuisine cuisine);
    }
}
