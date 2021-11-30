using CulinaryApi.Core.Entieties;
using CulinaryApi.Infrastructure.DTO.Recipes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CulinaryApi.Core.Repositories
{
    public interface IRecipeRepository
    {
        Task<Recipe> GetAsync(int id);
        Task<Recipe> GetAsync(string name);
        Task<IEnumerable<Recipe>> GetAllAsync(int? userId, RecipeQuery qery);
        Task AddAsync(Recipe recipe);
        Task UpdateAsync();
        Task DeleteAsync(Recipe recipe);
    }
}
