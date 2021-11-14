using System.ComponentModel.DataAnnotations;

namespace CulinaryApi.Infrastructure.DTO.Meals
{
    public class UpdateMealDto
    {
        [Required]
        public string Name { get; set; }
    }
}
