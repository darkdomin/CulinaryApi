using CulinaryApi.Core.Entieties;
using CulinaryApi.Infrastructure.DTO.Times;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryApi.Infrastructure.Services.Times
{
    public interface ITimeService
    {
        Task<TimeDto> GetAsync(int id);
        Task<TimeDto> GetAsync(string name);
        Task<IEnumerable<TimeDto>> BrowseAsync(string name = null);
        Task<int> CreateAsync(CreateTimeDto dto);
        Task UpdateAsync();
        Task DeleteAsync(int id);
    }
}
