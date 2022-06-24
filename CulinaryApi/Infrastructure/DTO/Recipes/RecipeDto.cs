namespace CulinaryApi.Infrastructure.DTO.Recipes
{
    public class RecipeDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Grammar { get; set; }
        public string Execution { get; set; }
        public string Photo { get; set; }
        public string ShortDescription { get; set; }

        public string MealId { get; set; }
        public string CuisineId { get; set; }
        public string DifficultId { get; set; }
        public string TimeId { get; set; }
    }
}
