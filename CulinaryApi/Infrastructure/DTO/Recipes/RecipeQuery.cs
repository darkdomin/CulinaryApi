namespace CulinaryApi.Infrastructure.DTO.Recipes
{
    public class RecipeQuery
    {
        public string SearchPhrase { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; }
        public SortDirection SortDirection { get; set; }

        public int Meal { get; set; } = 0;
        public int Time { get; set; } = 0;
        public int Level { get; set; } = 0;
        public int Cuisine { get; set; } = 0;
    }
}
