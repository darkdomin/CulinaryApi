using System.ComponentModel.DataAnnotations;

namespace CulinaryApi.Infrastructure.DTO.Cuisines
{
    public class UpdateCuisineDto
    {
        [Required]
        public string Name { get; set; }
    }
}
