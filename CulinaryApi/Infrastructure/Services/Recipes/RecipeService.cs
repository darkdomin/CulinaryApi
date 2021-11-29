using AutoMapper;
using CulinaryApi.Core.Entieties;
using CulinaryApi.Core.Repositories;
using CulinaryApi.Exceptions;
using CulinaryApi.Infrastructure.Authorization;
using CulinaryApi.Infrastructure.DTO.Recipes;
using CulinaryApi.Infrastructure.Extensions;
using CulinaryApi.Infrastructure.Services.Users;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CulinaryApi.Infrastructure.Services.Recipes
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public RecipeService(IRecipeRepository recipeRepository, IMapper mapper, IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }

        public async Task<RecipeDto> GetAsync(int id)
        {
            var recipe = await _recipeRepository.GetOrFailAsync(id);

            var authorizationResult = await _authorizationService.AuthorizeAsync(_userContextService.User, recipe,
                                      new ResourceOperationRequirement(ResourceOperation.READ));
            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

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

            var authorizationResult = await _authorizationService.AuthorizeAsync(_userContextService.User, recipe,
                                      new ResourceOperationRequirement(ResourceOperation.READ));
            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            var result = _mapper.Map<RecipeDto>(recipe);
            return result;
        }

        public async Task<IEnumerable<RecipeDto>> BrowseAsync(string searchPhrase = null)
        {
            var recipes = await _recipeRepository.GetAllAsync(_userContextService.GetUserId, searchPhrase);

            var result = _mapper.Map<IEnumerable<RecipeDto>>(recipes);

            return result;
        }

        public async Task<int> CreateAsync(CreateRecipeDto dto)
        {
            var newRecipe = _mapper.Map<Recipe>(dto);

            newRecipe.CreateById = _userContextService.GetUserId;

            await _recipeRepository.AddAsync(newRecipe);
            return newRecipe.Id;
        }

        public async Task UpdateAsync(UpdateRecipeDto dto, int id)
        {
            var recipe = await _recipeRepository.GetOrFailAsync(id);

            var authorizationResult = await _authorizationService.AuthorizeAsync(_userContextService.User, recipe,
                                      new ResourceOperationRequirement(ResourceOperation.UPDATE));
            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
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
            var recipe = await _recipeRepository.GetOrFailAsync(id);

            var authorizationResult = await _authorizationService.AuthorizeAsync(_userContextService.User, recipe,
                                      new ResourceOperationRequirement(ResourceOperation.DELETE));
            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            await _recipeRepository.DeleteAsync(recipe);
            await Task.CompletedTask;
        }
    }
}
