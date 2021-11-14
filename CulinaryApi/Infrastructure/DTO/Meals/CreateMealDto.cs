using System.ComponentModel.DataAnnotations;

namespace CulinaryApi.Infrastructure.DTO.Meals
{
    public class CreateMealDto
    {
        [Required]
        public string Name { get; set; }
    }
}
