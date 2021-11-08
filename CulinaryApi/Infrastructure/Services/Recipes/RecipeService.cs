using AutoMapper;
using CulinaryApi.Core.Entieties;
using CulinaryApi.Core.Repositories;
using CulinaryApi.Infrastructure.DTO.Recipes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CulinaryApi.Infrastructure.Services.Recipes
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;

        public RecipeService(IRecipeRepository recipeRepository, IMapper mapper)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
        }

        public async Task<RecipeDto> GetAsync(int id)
        {
            var recipe = await _recipeRepository.GetAsync(id);
            var result = _mapper.Map<RecipeDto>(recipe);
            return result;
        }

        public async Task<RecipeDto> GetAsync(string name)
        {
            var recipe = await _recipeRepository.GetAsync(name);
            var result = _mapper.Map<RecipeDto>(recipe);
            return result;
        }

        public async Task<IEnumerable<RecipeDto>> BrowseAsync(string name = null)
        {
            var recipes = await _recipeRepository.GetAllAsync(name);
            var resulta = _mapper.Map<IEnumerable<RecipeDto>>(recipes);
            return resulta;
        }

        public async Task<int> CreateAsync(CreateRecipeDto dto)
        {
            var newRecipe = _mapper.Map<Recipe>(dto);
            if (newRecipe == null)
            {
                throw new Exception();
            }
            await _recipeRepository.AddAsync(newRecipe);
            return newRecipe.Id;
        }

        public async Task UpdateAsync(UpdateRecipeDto dto, int id)
        {
            var recipe = await _recipeRepository.GetAsync(id);
            if(recipe == null)
            {
                throw new Exception();
            }
            recipe.SetName(dto.Name);
            recipe.SetGrammar(dto.Grammar);
            recipe.SetExecution(dto.Execution);
            recipe.SetMealId(dto.MealId);
            recipe.SetCuisineId(dto.CuisineId);
            recipe.SetDifficultId(dto.DifficultId);
            recipe.SetTimeId(dto.TimeId);
            await _recipeRepository.UpdateAsync();
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var recipe = await _recipeRepository.GetAsync(id);
            if (recipe == null)
            {
                throw new Exception();
            }
            await _recipeRepository.DeleteAsync(recipe);
            await Task.CompletedTask;
        }
    }
}
