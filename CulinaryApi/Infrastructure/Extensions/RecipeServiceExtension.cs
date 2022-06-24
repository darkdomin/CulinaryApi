using CulinaryApi.Core.Entieties;
using CulinaryApi.Exceptions;
using CulinaryApi.Infrastructure.DTO.Recipes;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CulinaryApi.Infrastructure.Extensions
{
    public class RecipeServiceExtension
    {
        //Find recipes by query
        public static IQueryable<Recipe> SearchEngine(RecipeQuery query, IQueryable<Recipe> recipes)
        {
            if (recipes == null)
            {
                throw new NotFoundException("Not found any recipes.");
            }

            if (query == null)
            {
                throw new ArgumentNullException($"The query cannot be {query}.");
            }

            return recipes.Where(r => r.Name.ToLower().Contains(query.SearchPhrase.ToLower()));
        }

        //Gets the specified number of recipes in reverse order
        public static IQueryable<Recipe> StartPage(IQueryable<Recipe> recipes, int takeRecipes) 
        {
            if (recipes == null)
            {
                throw new NotFoundException("Not found any recipes.");
            }

            return recipes.OrderByDescending(x => x.Id).Take(takeRecipes);

        }

        // method gets the list of favorites recipes
        public static IEnumerable<Recipe> FavoritesResult(IQueryable<Recipe> recipes, IQueryable<FavoriteRecipe> favorites)
        {
            if (recipes == null)
            {
                return Enumerable.Empty<Recipe>();
            }
            if (favorites == null)
            {
                throw new ArgumentNullException($"Favorit Recipe cannot by {favorites}!");
            }
            return favorites
                   .SelectMany(r => recipes
                   .Where(f => f.Id == r.RecipeId)
                   );
        }

        public static IQueryable<Recipe> Sorter(RecipeQuery query, IQueryable<Recipe> recipes)
        {
            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var columnSelector = new Dictionary<string, Expression<Func<Recipe, object>>>
                {
                    {nameof(Recipe.Name), r => r.Name },
                };
                var selectedColumn = columnSelector[query.SortBy];
                recipes = query.SortDirection == SortDirection.ASC ?
                          recipes.OrderBy(selectedColumn) :
                          recipes.OrderByDescending(selectedColumn);
            }

            return recipes;
        }

        //
        public static void AuthorizationForbidden(AuthorizationResult authorizationResult)
        {
            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }
        }
    }
}
