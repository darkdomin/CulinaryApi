using System.ComponentModel.DataAnnotations;

namespace CulinaryApi.Infrastructure.DTO.Difficulties
{
    public class CreateDifficultyDto
    {
        [Required]
        public string Name { get; set; }
    }
}
