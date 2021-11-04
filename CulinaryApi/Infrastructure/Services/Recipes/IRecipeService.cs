using CulinaryApi.Infrastructure.DTO.Recipes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CulinaryApi.Infrastructure.Services
{
    public interface IRecipeService
    {
        Task<RecipeDto> GetAsync(int id);
        Task<RecipeDto> GetAsync(string name);
        Task<IEnumerable<RecipeDto>> BrowseAsync(string name = null);
        Task<int> CreateAsync(CreateRecipeDto dto);
        Task UpdateAsync();
        Task DeleteAsync(int id);
    }
}
