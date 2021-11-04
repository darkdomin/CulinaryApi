namespace CulinaryApi.Infrastructure.DTO.Recipes
{
    public class CreateRecipeDto
    {
        public string Name { get; set; }
        public string Grammar { get; set; }
        public string Execution { get; set; }
        public int MealId { get; set; }
    }
}
