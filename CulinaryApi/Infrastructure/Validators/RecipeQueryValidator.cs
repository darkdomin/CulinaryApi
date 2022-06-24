using CulinaryApi.Core.Entieties;
using CulinaryApi.Core.Repositories;
using CulinaryApi.Infrastructure.DTO.Recipes;
using FluentValidation;
using System;
using System.Linq;

namespace CulinaryApi.Infrastructure.Validators
{
    public class RecipeQueryValidator : AbstractValidator<RecipeQuery>
    {
        // private int[] allowedPageSize = new[] { 12 };
        private readonly int allowedPageSize =  12 ;
        private string[] allowedSortBycolumnNames = new[] { nameof(Recipe.Name), nameof(Recipe.Grammar) };
        public RecipeQueryValidator(IRecipeRepository recipeRepository)
        {
            bool dontGet = false;
            RuleFor(r => r.PageSize).Custom((value, context) =>
              {
                  if (value == 0) dontGet = true;
              });
            RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(r => r.PageSize).Custom((value, context) =>
            {
                //if (!allowedPageSize.Contains(value) && dontGet == false)
                //{
                if (allowedPageSize != value && dontGet == false)
                {
                    context.AddFailure("PageSize", $"Page Size must in [{string.Join(",", allowedPageSize)}]");
                }
            });

            RuleFor(r => r.SortBy).Must(value => string.IsNullOrEmpty(value) || allowedSortBycolumnNames.Contains(value))
                .WithMessage($"SortBy is optional or must be in [{string.Join(",", allowedSortBycolumnNames)}]");
        }
    }
}
