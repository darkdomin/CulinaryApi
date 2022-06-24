using CulinaryApi.Core.Entieties;
using CulinaryApi.Infrastructure.DTO.FilterDto;
using CulinaryApi.Infrastructure.Services.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CulinaryApi.Controllers
{
    [Route("api/recipes/meals")]
    [ApiController]
    public class MealController : ControllerBase
    {
        private readonly IFilterService<Meal> _mealService;

        public MealController(IFilterService<Meal> mealService)
        {
            _mealService = mealService;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult> Get([FromRoute] int id)
        {
            var time = await _mealService.GetAsync(id);
            return Ok(time);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Get()
        {
            var meals = await _mealService.BrowseAsync();
            return Ok(meals);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create([FromBody] CreateFilterDto<Meal> dto)
        {
            var mealId = await _mealService.CreateAsync(dto);
            return Created($"api/meals/{mealId}", null);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Update([FromBody] UpdateFilterDto<Meal> dto, int id)
        {
            await _mealService.UpdateAsync(dto, id);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _mealService.DeleteAsync(id);
            return NoContent();
        }
    }
}
