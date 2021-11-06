namespace CulinaryApi.Core.Entieties
{
    public class Recipe
    {
        public int Id { get; protected set; }
        public string Name { get; protected set; }
        public string Grammar { get; protected set; }
        public string Execution { get; protected set; }

        public int MealId { get; protected set; }
        public virtual Meal Meal { get; protected set; }
        public int CuisineId { get; protected set; }
        public virtual Cuisine Cuisine { get; protected set; }
        public int DifficultId { get; protected set; }
        public virtual Difficulty Difficult { get; protected set; }
        public int TimeId { get; protected set; }
        public virtual Time Time { get; protected set; }
    }
}
