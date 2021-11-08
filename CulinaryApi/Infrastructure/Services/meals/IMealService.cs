using CulinaryApi.Infrastructure.DTO.Meals;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CulinaryApi.Infrastructure.Services.meals
{
    public interface IMealService
    {
        Task<MealDto> GetAsync(int id);
        Task<MealDto> GetAsync(string name);
        Task<IEnumerable<MealDto>> BrowseAsync(string name = null);
        Task<int> CreateAsync(CreateMealDto dto);
        Task UpdateAsync(UpdateMealDto dto, int id);
        Task DeleteAsync(int id);
    }
}
