using CulinaryApi.Infrastructure.DTO.FilterDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CulinaryApi.Infrastructure.Services.Filters
{
    public interface IFilterService<TEntity> where TEntity : class
    {
        Task<FilterDto<TEntity>> GetAsync(int id);
        Task<FilterDto<TEntity>> GetAsync(string name);
        Task<IEnumerable<FilterDto<TEntity>>> BrowseAsync(string name = null);
        Task<int> CreateAsync(CreateFilterDto<TEntity> dto);
        Task UpdateAsync(UpdateFilterDto<TEntity> dto, int id);
        Task DeleteAsync(int id);
    }
}
