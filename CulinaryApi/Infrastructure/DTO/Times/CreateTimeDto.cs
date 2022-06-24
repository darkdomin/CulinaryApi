using System.ComponentModel.DataAnnotations;

namespace CulinaryApi.Infrastructure.DTO.Times
{
    public class CreateTimeDto
    {
        [Required]
        public string Name { get; set; }
    }
}
