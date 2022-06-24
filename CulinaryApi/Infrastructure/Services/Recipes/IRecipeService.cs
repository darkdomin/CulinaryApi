using CulinaryApi.Infrastructure.DTO;
using CulinaryApi.Infrastructure.DTO.Recipes;
using System.Threading.Tasks;

namespace CulinaryApi.Infrastructure.Services
{
    public interface IRecipeService
    {
        Task<RecipeDto> GetAsync(int id);
        Task<PagedResult<RecipeDto>> BrowseAsync(RecipeQuery dto);
        Task<PagedResult<RecipeDto>> BrowseHomeAsync(RecipeQuery dto);
        Task<int> CreateAsync(CreateRecipeDto dto);
        Task UpdateAsync(UpdateRecipeDto dto, int id);
        Task DeleteAsync(int id);
    }
}
