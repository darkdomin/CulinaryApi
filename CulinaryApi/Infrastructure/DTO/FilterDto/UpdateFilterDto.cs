using System.ComponentModel.DataAnnotations;

namespace CulinaryApi.Infrastructure.DTO.FilterDto
{
    public class UpdateFilterDto<TEntity>
    {
        [Required]
        public string Name { get; set; }
    }
}
