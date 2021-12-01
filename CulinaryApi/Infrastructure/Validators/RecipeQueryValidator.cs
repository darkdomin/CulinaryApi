using CulinaryApi.Infrastructure.DTO.Recipes;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryApi.Infrastructure.Validators
{
    public class RecipeQueryValidator : AbstractValidator<RecipeQuery>
    {
        private int[] allowedPageSize = new[] { 5, 10, 15 };
        public RecipeQueryValidator()
        {
            RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(r => r.PageSize).Custom((value, context) =>
            {
                if (!allowedPageSize.Contains(value))
                {
                    context.AddFailure("PageSize", $"Page Size must in [{string.Join(",",allowedPageSize)}]" );
                }
            });
        }
    }
}
