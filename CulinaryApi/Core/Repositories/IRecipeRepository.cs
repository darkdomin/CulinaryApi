using CulinaryApi.Core.Entieties;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CulinaryApi.Core.Repositories
{
    public interface IRecipeRepository
    {
        Task<Recipe> GetAsync(int id);
        Task<Recipe> GetAsync(string name);
        Task<IEnumerable<Recipe>> GetAllAsync(int? userId, string name = "");
        Task AddAsync(Recipe recipe);
        Task UpdateAsync();
        Task DeleteAsync(Recipe recipe);
    }
}
