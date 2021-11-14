using System.ComponentModel.DataAnnotations;

namespace CulinaryApi.Infrastructure.DTO.Cuisines
{
    public class CreateCuisineDto
    {
        [Required]
        public string Name { get; set; }
    }
}
