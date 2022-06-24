using CulinaryApi.Core.Entieties;
using CulinaryApi.Infrastructure.DTO.FilterDto;
using CulinaryApi.Infrastructure.Services.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CulinaryApi.Controllers
{
    [Route("api/recipes/difficultyLevel")]
    [ApiController]
    public class DifficultyController : ControllerBase
    {
        private readonly IFilterService<Difficulty> _difficultyService;

        public DifficultyController(IFilterService<Difficulty> difficultyService)
        {
            _difficultyService = difficultyService;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult> Get([FromRoute] int id)
        {
            var time = await _difficultyService.GetAsync(id);
            return Ok(time);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Get()
        {
            var difficulties = await _difficultyService.BrowseAsync();
            return Ok(difficulties);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create([FromBody] CreateFilterDto<Difficulty> dto)
        {
            var difficultyId = await _difficultyService.CreateAsync(dto);
            return Created($"api/difficultyLevel/{difficultyId}", null);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Update([FromBody] UpdateFilterDto<Difficulty> dto, int id)
        {
            await _difficultyService.UpdateAsync(dto, id);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _difficultyService.DeleteAsync(id);
            return NoContent();
        }
    }
}
