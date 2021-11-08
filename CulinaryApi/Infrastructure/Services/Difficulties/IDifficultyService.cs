using CulinaryApi.Infrastructure.DTO.Difficulties;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CulinaryApi.Infrastructure.Services.Difficulties
{
    public interface IDifficultyService
    {
        Task<DifficultyDto> GetAsync(int id);
        Task<DifficultyDto> GetAsync(string name);
        Task<IEnumerable<DifficultyDto>> BrowseAsync(string name = null);
        Task<int> CreateAsync(CreateDifficultyDto dto);
        Task UpdateAsync(UpdateDifficultyDto dto, int id);
        Task DeleteAsync(int id);
    }
}
