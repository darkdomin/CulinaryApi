namespace CulinaryApi.Infrastructure.DTO.Recipes
{
    public class FIlterQuery
    {
        public int MealId { get; set; }
        public int CuisineId { get; set; }
        public int DifficultId { get; set; } 
        public int TimeId { get; set; } 
    }
}
