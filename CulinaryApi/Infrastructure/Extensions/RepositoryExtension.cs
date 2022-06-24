using CulinaryApi.Core.Entieties;
using CulinaryApi.Core.Repositories;
using CulinaryApi.Exceptions;
using System.Threading.Tasks;

namespace CulinaryApi.Infrastructure.Extensions
{
    public static class RepositoryExtension
    {
        public static async Task<Recipe> GetOrFailAsync(this IRecipeRepository repository, int id)
        {
            var recipe = await repository.GetAsync(id);
            if (recipe == null)
            {
                throw new NotFoundException($"Recipe with {id} not found.");
            }
            return recipe;
        }

        public static async Task<TEntity> GetOrFailAsync<TEntity>(this IFilterRepository<TEntity> repository, int id) where TEntity : Filter<TEntity>
        {
            var filter = await repository.GetAsync(id);
            if (filter == null)
            {
                throw new NotFoundException("FIlter not found.");
            }
            return filter;
        }
    }
}
