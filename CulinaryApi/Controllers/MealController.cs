using CulinaryApi.Infrastructure.DTO.Meals;
using CulinaryApi.Infrastructure.Services.meals;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CulinaryApi.Controllers
{
    [Route("api/recipe/meal")]
    [Authorize(Roles = "Admin")]
    public class MealController : ControllerBase
    {
        private readonly IMealService _mealService;

        public MealController(IMealService mealService)
        {
            _mealService = mealService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get([FromRoute] int id)
        {
            var time = await _mealService.GetAsync(id);
            return Ok(time);
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var meals = await _mealService.BrowseAsync();
            return Ok(meals);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateMealDto dto)
        {
            var mealId = await _mealService.CreateAsync(dto);
            return Created($"api/recipe/meal/{mealId}", null);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromBody] UpdateMealDto dto, int id)
        {
            await _mealService.UpdateAsync(dto, id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _mealService.DeleteAsync(id);
            return NoContent();
        }
    }
}
