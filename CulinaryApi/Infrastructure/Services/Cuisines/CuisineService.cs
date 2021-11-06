using AutoMapper;
using CulinaryApi.Core.Entieties;
using CulinaryApi.Core.Repositories;
using CulinaryApi.Infrastructure.DTO.Cuisines;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CulinaryApi.Infrastructure.Services.Cuisines
{
    public class CuisineService : ICuisineService
    {
        private readonly ICuisineRepository _cuisineRepository;
        private readonly IMapper _mapper;
        public CuisineService(ICuisineRepository cuisineRepository, IMapper mapper)
        {
            _cuisineRepository = cuisineRepository;
            _mapper = mapper;
        }

        public async Task<CuisineDto> GetAsync(int id)
        {
            var cuisine = await _cuisineRepository.GetAsync(id);
            var result = _mapper.Map<CuisineDto>(cuisine);
            return result;
        }

        public async Task<CuisineDto> GetAsync(string name)
        {
            var cuisine = await _cuisineRepository.GetAsync(name);
            var result = _mapper.Map<CuisineDto>(cuisine);
            return result;
        }
        public async Task<IEnumerable<CuisineDto>> BrowseAsync(string name = null)
        {
            var cuisines = await _cuisineRepository.GetAllAsync(name);
            var result = _mapper.Map<IEnumerable<CuisineDto>>(cuisines);
            return result;
        }

        public async Task<int> CreateAsync(CreateCuisineDto dto)
        {
            var newcuisine = _mapper.Map<Cuisine>(dto);
            await _cuisineRepository.AddAsync(newcuisine);
            return newcuisine.Id;
        }

        public Task UpdateAsync()
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            var cuisine = await _cuisineRepository.GetAsync(id);
            await _cuisineRepository.DeleteAsync(cuisine);
            await Task.CompletedTask;
        }
    }
}
