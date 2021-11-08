namespace CulinaryApi.Infrastructure.DTO.Recipes
{
    public class UpdateRecipeDto
    {
        public string Name { get; set; }
        public string Grammar { get; set; }
        public string Execution { get; set; }
        public int MealId { get; set; }
        public int CuisineId { get; set; }
        public int DifficultId { get; set; }
        public int TimeId { get; set; }
    }
}
