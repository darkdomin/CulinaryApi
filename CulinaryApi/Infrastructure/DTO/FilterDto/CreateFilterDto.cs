using System.ComponentModel.DataAnnotations;

namespace CulinaryApi.Infrastructure.DTO.FilterDto
{
    public class CreateFilterDto<T>
    {
        [Required]
        public string Name { get; set; }
    }
}
