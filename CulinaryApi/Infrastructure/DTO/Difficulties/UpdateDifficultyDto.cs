using System.ComponentModel.DataAnnotations;

namespace CulinaryApi.Infrastructure.DTO.Difficulties
{
    public class UpdateDifficultyDto
    {
        [Required]
        public string Name { get; set; }
    }
}
