using CulinaryApi.Infrastructure.DTO.Cuisines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryApi.Infrastructure.Services.Cuisines
{
    public interface ICuisineService
    {
        Task<CuisineDto> GetAsync(int id);
        Task<CuisineDto> GetAsync(string name);
        Task<IEnumerable<CuisineDto>> BrowseAsync(string name = null);
        Task<int> CreateAsync(CreateCuisineDto dto);
        Task UpdateAsync();
        Task DeleteAsync(int id);
    }
}
