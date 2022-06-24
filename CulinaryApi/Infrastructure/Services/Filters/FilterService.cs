using AutoMapper;
using CulinaryApi.Core.Entieties;
using CulinaryApi.Core.Repositories;
using CulinaryApi.Exceptions;
using CulinaryApi.Infrastructure.DTO.FilterDto;
using CulinaryApi.Infrastructure.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CulinaryApi.Infrastructure.Services.Filters
{
    public class FilterService<TEntity> : IFilterService<TEntity> where TEntity : Filter<TEntity>
    {
        private readonly IFilterRepository<TEntity> _filterRepository;
        private readonly IMapper _mapper;

        public FilterService(IMapper mapper, IFilterRepository<TEntity> filterRepository)
        {
            _filterRepository = filterRepository;
            _mapper = mapper;
        }

        public async Task<FilterDto<TEntity>> GetAsync(int id)
        {
            var filter = await _filterRepository.GetOrFailAsync(id);
            var result = _mapper.Map<FilterDto<TEntity>>(filter);
            return result;
        }

        public async Task<FilterDto<TEntity>> GetAsync(string name)
        {
            var filter = await _filterRepository.GetAsync(name);
            if (filter == null)
            {
                throw new NotFoundException("Meal not found.");
            }
            var result = _mapper.Map<FilterDto<TEntity>>(filter);
            return result;
        }
        public async Task<IEnumerable<FilterDto<TEntity>>> BrowseAsync(string name = null)
        {
            var filters = await _filterRepository.GetAllAsync(name);
            var resulta = _mapper.Map<IEnumerable<FilterDto<TEntity>>>(filters);
            return resulta;
        }

        public async Task<int> CreateAsync(CreateFilterDto<TEntity> dto)
        {
            var newFilter = _mapper.Map<TEntity>(dto);
            await _filterRepository.AddAsync(newFilter);
            return newFilter.Id;
        }

        public async Task UpdateAsync(UpdateFilterDto<TEntity> dto, int id)
        {
            var filter = await _filterRepository.GetOrFailAsync(id);
            filter.SetName(dto.Name);
            await _filterRepository.UpdateAsync();
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var filter = await _filterRepository.GetOrFailAsync(id);
            await _filterRepository.DeleteAsync(filter);
            await Task.CompletedTask;
        }
    }
}
