using CulinaryApi.Core.Entieties;
using CulinaryApi.Infrastructure.DTO.Recipes;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryApi.Core.Repositories
{
    public interface IRecipeRepository
    {
        Task<Recipe> GetAsync(int id);
        Task<IQueryable<Recipe>> GetAllAsync(int? userId, RecipeQuery qery); 
        Task AddAsync(Recipe recipe);
        Task UpdateAsync();
        Task DeleteAsync(Recipe recipe);
        Task<IQueryable<Recipe>> GetAll(int? id);
    }
}
