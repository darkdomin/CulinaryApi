using AutoMapper;
using CulinaryApi.Core.Entieties;
using CulinaryApi.Core.Repositories;
using CulinaryApi.Infrastructure.DTO.Meals;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CulinaryApi.Infrastructure.Services.meals
{
    public class MealService : IMealService
    {
        private readonly IMealRepository _mealRepository;
        private readonly IMapper _mapper;
        public MealService(IMealRepository mealRepository, IMapper mapper)
        {
            _mealRepository = mealRepository;
            _mapper = mapper;
        }

        public async Task<MealDto> GetAsync(int id)
        {
            var meal = await _mealRepository.GetAsync(id);
            var result = _mapper.Map<MealDto>(meal);
            return result;
        }

        public async Task<MealDto> GetAsync(string name)
        {
            var meal = await _mealRepository.GetAsync(name);
            var result = _mapper.Map<MealDto>(meal);
            return result;
        }
        public async Task<IEnumerable<MealDto>> BrowseAsync(string name = null)
        {
            var meals = await _mealRepository.GetAllAsync(name);
            var resulta = _mapper.Map<IEnumerable<MealDto>>(meals);
            return resulta;
        }

        public async Task<int> CreateAsync(CreateMealDto dto)
        {
            var newMeal = _mapper.Map<Meal>(dto);
            await _mealRepository.AddAsync(newMeal);
            return newMeal.Id;
        }

        public async Task UpdateAsync(UpdateMealDto dto, int id)
        {
            var meal = await _mealRepository.GetAsync(id);
            meal.SetName(dto.Name);
            await _mealRepository.UpdateAsync();
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var meal = await _mealRepository.GetAsync(id);
            await _mealRepository.DeleteAsync(meal);
            await Task.CompletedTask;
        }
    }
}
