namespace CulinaryApi.Infrastructure.DTO.Recipes
{
    public class RecipeQuery
    {
        public string SearchPhrase { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; }
        public SortDirection SortDirection { get; set; }
        public bool IsHome { get; set; }

        public int Meal { get; set; } = 0;
        public int Time { get; set; }
        public int Level { get; set; }
        public int Cuisine { get; set; }
    }
}
