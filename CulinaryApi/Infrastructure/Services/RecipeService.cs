using AutoMapper;
using CulinaryApi.Core.Entieties;
using CulinaryApi.Core.Repositories;
using CulinaryApi.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CulinaryApi.Infrastructure.Services
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
            await _recipeRepository.AddAsync(newRecipe);
            return newRecipe.Id;
        }

        public Task UpdateAsync()
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            var recipe = await _recipeRepository.GetAsync(id);
            await _recipeRepository.DeleteAsync(recipe);
            await Task.CompletedTask;
        }
    }
}
