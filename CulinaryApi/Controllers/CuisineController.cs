using CulinaryApi.Core.Entieties;
using CulinaryApi.Infrastructure.DTO.FilterDto;
using CulinaryApi.Infrastructure.Services.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CulinaryApi.Controllers
{
    [Route("api/recipes/cuisines")]
    [ApiController]
    public class CuisineController : ControllerBase
    {
        private readonly IFilterService<Cuisine> _cuisineService;

        public CuisineController(IFilterService<Cuisine> cuisineService)
        {
            _cuisineService = cuisineService;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult> Get([FromRoute] int id)
        {
            var time = await _cuisineService.GetAsync(id);
            return Ok(time);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Get()
        {
            var cuisines = await _cuisineService.BrowseAsync();
            return Ok(cuisines);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create([FromBody] CreateFilterDto<Cuisine> dto)
        {
            var cuisineId = await _cuisineService.CreateAsync(dto);
            return Created($"api/cuisines/{cuisineId}", null);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Update([FromBody] UpdateFilterDto<Cuisine> dto, int id)
        {
            await _cuisineService.UpdateAsync(dto, id);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _cuisineService.DeleteAsync(id);
            return NoContent();
        }
    }
}
