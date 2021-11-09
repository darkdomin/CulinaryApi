using CulinaryApi.Core.Entieties;
using CulinaryApi.Core.Repositories;
using CulinaryApi.Exceptions;
using System.Threading.Tasks;

namespace CulinaryApi.Infrastructure.Extensions
{
    public static class RepositoryExtensions
    {
        public static async Task<Recipe> GetOrFailAsync(this IRecipeRepository repository, int id)
        {
            var recipe = await repository.GetAsync(id);
            if (recipe == null)
            {
                throw new NotFoundException("Recipe not found.");
            }
            return recipe;
        }

        public static async Task<Meal> GetOrFailAsync(this IMealRepository repository, int id)
        {
            var meal = await repository.GetAsync(id);
            if (meal == null)
            {
                throw new NotFoundException("Meal not found.");
            }
            return meal;
        }
        public static async Task<Cuisine> GetOrFailAsync(this ICuisineRepository repository, int id)
        {
            var cuisine = await repository.GetAsync(id);
            if (cuisine == null)
            {
                throw new NotFoundException("Cuisine not found.");
            }
            return cuisine;
        }

        public static async Task<Difficulty> GetOrFailAsync(this IDifficultyRepository repository, int id)
        {
            var difficulty = await repository.GetAsync(id);
            if (difficulty == null)
            {
                throw new NotFoundException("Difficulty not found.");
            }
            return difficulty;
        }

        public static async Task<Time> GetOrFailAsync(this ITimeRepository repository, int id)
        {
            var time = await repository.GetAsync(id);
            if (time == null)
            {
                throw new NotFoundException("Time not found.");
            }
            return time;
        }
    }
}
