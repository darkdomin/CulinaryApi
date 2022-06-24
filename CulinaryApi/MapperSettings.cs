using AutoMapper;
using CulinaryApi.Core.Entieties;
using CulinaryApi.Infrastructure.DTO.FilterDto;
using CulinaryApi.Infrastructure.DTO.Meals;
using CulinaryApi.Infrastructure.DTO.Recipes;
using CulinaryApi.Infrastructure.DTO.Users;

namespace CulinaryApi
{
    public class MapperSettings : Profile
    {
        public MapperSettings()
        {
            // --- Recipe --- //
            CreateMap<CreateRecipeDto, Recipe>();
  
            CreateMap<Recipe, RecipeDto>()
                .ForMember(d => d.MealId, m => m.MapFrom(r => r.Meal.Name))
                .ForMember(t => t.TimeId, m => m.MapFrom(r => r.Time.Name))
                .ForMember(dto => dto.DifficultId, m => m.MapFrom(d => d.Difficult.Name))
                .ForMember(d => d.CuisineId, m => m.MapFrom(c => c.Cuisine.Name));

            // --- Filter mapper --- //
            CreateMap(typeof(Filter<Meal>), typeof(FilterDto<Meal>));
            CreateMap(typeof(Filter<Cuisine>), typeof(FilterDto<Cuisine>));
            CreateMap(typeof(Filter<Difficulty>), typeof(FilterDto<Difficulty>));
            CreateMap(typeof(Filter<Time>), typeof(FilterDto<Time>));

            // --- User --- //
            CreateMap<User, UserDto>();
        }
    }
}
