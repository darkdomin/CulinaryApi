using AutoMapper;
using CulinaryApi.Core.Entieties;
using CulinaryApi.Infrastructure.DTO.Cuisines;
using CulinaryApi.Infrastructure.DTO.Difficulties;
using CulinaryApi.Infrastructure.DTO.Meals;
using CulinaryApi.Infrastructure.DTO.Recipes;
using CulinaryApi.Infrastructure.DTO.Times;

namespace CulinaryApi
{
    public class MapperSettings : Profile
    {
        public MapperSettings()
        {
            CreateMap<CreateRecipeDto, Recipe>();
            CreateMap<Recipe, RecipeDto>()
                .ForMember(d => d.Meal, m => m.MapFrom(r => r.Meal.Name))
                .ForMember(t => t.Time, m => m.MapFrom(r => r.Time.Name))
                .ForMember(dto => dto.Difficulty, m => m.MapFrom(d => d.Difficult.Name))
                .ForMember(d => d.Cuisine, m => m.MapFrom(c => c.Cuisine.Name));

            CreateMap<Time, TimeDto>()
                .ForMember(d => d.Time, m => m.MapFrom(t => t.Name)); //??? inaczej nie działa
            CreateMap<CreateTimeDto, Time>();

            CreateMap<CreateMealDto, Meal>();
            CreateMap<Meal, MealDto>();

            CreateMap<CreateCuisineDto, Cuisine>();
            CreateMap<Cuisine, CuisineDto>();

            CreateMap<CreateDifficultyDto, Difficulty>();
            CreateMap<Difficulty, DifficultyDto>();
        }
    }
}
