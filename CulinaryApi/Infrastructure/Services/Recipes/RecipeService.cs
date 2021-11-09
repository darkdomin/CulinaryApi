﻿using AutoMapper;
using CulinaryApi.Core.Entieties;
using CulinaryApi.Core.Repositories;
using CulinaryApi.Exceptions;
using CulinaryApi.Infrastructure.DTO.Recipes;
using CulinaryApi.Infrastructure.Extensions;
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
            var recipe = await _recipeRepository.GetOrFailAsync(id);
            var result = _mapper.Map<RecipeDto>(recipe);
            return result;
        }

        public async Task<RecipeDto> GetAsync(string name)
        {
            var recipe = await _recipeRepository.GetAsync(name);
            if (recipe == null)
            {
                throw new NotFoundException("Recipe not found.");
            }
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

        public async Task UpdateAsync(UpdateRecipeDto dto, int id)
        {
            var recipe = await _recipeRepository.GetOrFailAsync(id);
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
            var recipe = await _recipeRepository.GetOrFailAsync(id);
            await _recipeRepository.DeleteAsync(recipe);
            await Task.CompletedTask;
        }
    }
}