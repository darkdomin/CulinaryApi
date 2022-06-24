using System.ComponentModel.DataAnnotations;

namespace CulinaryApi.Infrastructure.DTO.Times
{
    public class UpdateTimeDto
    {
        [Required]
        public string Name { get; set; }
    }
}
