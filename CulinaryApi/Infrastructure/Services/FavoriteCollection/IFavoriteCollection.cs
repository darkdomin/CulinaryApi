using CulinaryApi.Core.Entieties;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CulinaryApi.Infrastructure.Services.FavoriteCollection
{
    public interface IFavoriteCollection
    {
        Task Add(FavoriteRecipe favoriteRecipe);
        Task Remove(FavoriteRecipe favoriteRecipe);
        Task<FavoriteRecipe> GetId(int recipeId);
        Task<IEnumerable<FavoriteRecipe>> GetAll();
        Task<IEnumerable<FavoriteRecipe>> GetFavorites();
        Task Update(FavoriteRecipe favoriteRecipe);
        Task DeleteAll();
    }
}
