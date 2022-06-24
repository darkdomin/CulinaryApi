namespace CulinaryApi.Core.Entieties
{
    public class FavoriteRecipe : Identity
    {
        public int Counter { get; protected set; }
        public int RecipeId { get; protected set; }

        protected FavoriteRecipe(int recipeId)
        {
            RecipeId = recipeId;
        }

        protected FavoriteRecipe()
        {

        }

        public void IncreaseCounter()
        {
            Counter++;
        }

        public static FavoriteRecipe Creator(int recipeId)
        {
            return new FavoriteRecipe(recipeId);
        }
    }
}
