using AutoMapper;
using CulinaryApi.Core.Entieties;
using CulinaryApi.Core.Repositories;
using CulinaryApi.Infrastructure.Authorization;
using CulinaryApi.Infrastructure.DTO;
using CulinaryApi.Infrastructure.DTO.Recipes;
using CulinaryApi.Infrastructure.Extensions;
using CulinaryApi.Infrastructure.Services.FavoriteCollection;
using CulinaryApi.Infrastructure.Services.Users;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CulinaryApi.Infrastructure.Services.Recipes
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;
        private readonly TestEmail _testEmail;
        private readonly IFavoriteCollection _favoriteCollection;

        public RecipeService(IRecipeRepository recipeRepository, IMapper mapper, IAuthorizationService authorizationService, IUserContextService userContextService, TestEmail testEmail, IFavoriteCollection favoriteCollection
           )
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
            _testEmail = testEmail;
            _favoriteCollection = favoriteCollection;
        }

        public async Task<RecipeDto> GetAsync(int id)
        {
            var recipe = await GetAuthorizedRecipe(id);

            var favRecipe = await _favoriteCollection.GetId(recipe.Id);

            if (favRecipe == null)
            {
                favRecipe = FavoriteRecipe.Creator(recipe.Id);
                await _favoriteCollection.Add(favRecipe);
            }

            favRecipe.IncreaseCounter();
            await _favoriteCollection.Update(favRecipe);

            return _mapper.Map<RecipeDto>(recipe);
        }

        public async Task<PagedResult<RecipeDto>> BrowseAsync(RecipeQuery query)
        {
            var recipes = await _recipeRepository.GetAllAsync(_userContextService.GetUserId, query);

            var totalCount = recipes.Count();

            if (!string.IsNullOrEmpty(query.SearchPhrase))
            {
                recipes = RecipeServiceExtension.SearchEngine(query, recipes);
            }

            recipes = recipes
                      .Skip(query.PageSize * (query.PageNumber - 1))
                      .Take(query.PageSize);

            //var recipesDtos = SorterAndConvertToDto(query, ref recipes);
            var recipesDtos = _mapper.Map<List<RecipeDto>>(recipes);
            return new PagedResult<RecipeDto>(recipesDtos, totalCount, query.PageSize, query.PageNumber);
        }

        public async Task<PagedResult<RecipeDto>> BrowseHomeAsync(RecipeQuery query)
        {
            var recipes = await _recipeRepository.GetAllAsync(_userContextService.GetUserId, query);
            var totalCount = recipes.Count();

            recipes = RecipeServiceExtension.StartPage(recipes, 6);

            // var recipesDtos = SorterAndConvertToDto(query, ref recipes);
            var recipesDtos = _mapper.Map<List<RecipeDto>>(recipes);
            return new PagedResult<RecipeDto>(recipesDtos, totalCount, query.PageSize, query.PageNumber);
        }


        public async Task<PagedResult<RecipeDto>> BrowseFavoriteAsync(RecipeQuery query)
        {
            var recipes = await _recipeRepository.GetAllAsync(_userContextService.GetUserId, query);
            var totalCount = recipes.Count();

            var favorits = await _favoriteCollection.GetFavorites();

            var result = RecipeServiceExtension.FavoritesResult(recipes, favorits.AsQueryable());
            recipes = result.AsQueryable();
            int favortisNumber = result.Count();

            if (favortisNumber < 3)
            {
                recipes = null;
            }

            // var recipesDtos = SorterAndConvertToDto(query, ref recipes);
            var recipesDtos = _mapper.Map<List<RecipeDto>>(recipes);
            return new PagedResult<RecipeDto>(recipesDtos, totalCount, query.PageSize, query.PageNumber);
        }

        public async Task<int> CreateAsync(CreateRecipeDto dto)
        {
            var newRecipe = _mapper.Map<Recipe>(dto);

            string email = GetUserEmail();

            newRecipe.SetCreateBy(_userContextService.GetUserId);

            if (email == _testEmail.Email)
            {
                var result = await _recipeRepository.GetAll(_userContextService.GetUserId);
                if (result.Count() < _testEmail.Max)
                {

                    await _recipeRepository.AddAsync(newRecipe);
                }
            }
            else
            {
                await _recipeRepository.AddAsync(newRecipe);
            }

            var favRecipe = FavoriteRecipe.Creator(newRecipe.Id);
            await _favoriteCollection.Add(favRecipe);

            return newRecipe.Id;
        }

        public async Task UpdateAsync(UpdateRecipeDto dto, int id)
        {
            var recipe = await GetAuthorizedRecipe(id);

            recipe.SetName(dto.Name);
            recipe.SetGrammar(dto.Grammar);
            recipe.SetExecution(dto.Execution);
            recipe.SetPhoto(dto.Photo);
            recipe.SetShortDescription(dto.ShortDescription);
            recipe.SetMealId(dto.MealId);
            recipe.SetCuisineId(dto.CuisineId);
            recipe.SetDifficultId(dto.DifficultId);
            recipe.SetTimeId(dto.TimeId);
            await _recipeRepository.UpdateAsync();

            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var recipe = await GetAuthorizedRecipe(id);

            string email = GetUserEmail();
            var userId = _userContextService.GetUserId;

            var favRecipe = await _favoriteCollection.GetId(recipe.Id);

            await _favoriteCollection.Remove(favRecipe);
            if (email == _testEmail.Email)
            {
                var result = await _recipeRepository.GetAll(userId);
                var resultCount = result.Count();
                if (resultCount < _testEmail.Min)
                {
                    await _recipeRepository.DeleteAsync(recipe);
                }
            }
            else
            {
                await _recipeRepository.DeleteAsync(recipe);
            }
        }

        // -- private method --//

        private string GetUserEmail()
        {
            return _userContextService.User.FindFirst(x => x.Type == ClaimTypes.Name).Value;
        }

        private async Task<AuthorizationResult> Authorization(Recipe recipe)
        {
            return await _authorizationService.AuthorizeAsync(_userContextService.User, recipe,
                         new ResourceOperationRequirement(ResourceOperation.READ));
        }

        private async Task<Recipe> GetAuthorizedRecipe(int id)
        {
            var recipe = await _recipeRepository.GetOrFailAsync(id);

            var authorizationResult = await Authorization(recipe);

            RecipeServiceExtension.AuthorizationForbidden(authorizationResult);
            return recipe;
        }

        private List<RecipeDto> SorterAndConvertToDto(RecipeQuery query, ref IQueryable<Recipe> recipes)
        {
            recipes = RecipeServiceExtension.Sorter(query, recipes);

            var recipesDtos = _mapper.Map<List<RecipeDto>>(recipes);
            return recipesDtos;
        }
    }
}
