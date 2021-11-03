using AutoMapper;
using CulinaryApi.Core.Entieties;
using CulinaryApi.Infrastructure.DTO;

namespace CulinaryApi
{
    public class MapperSettings : Profile
    {
        public MapperSettings()
        {
            CreateMap<CreateRecipeDto, Recipe>()
                .ForMember(r => r.Meal.Id, m => m.MapFrom(d => d.MealId));
            CreateMap<Recipe, RecipeDto>()
                .ForMember(d => d.Meal, m => m.MapFrom(r => r.Meal.Name));  
        }
    }
}
