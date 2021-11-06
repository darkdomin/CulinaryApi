using CulinaryApi.Infrastructure.DTO.Recipes;
using CulinaryApi.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CulinaryApi.Controllers
{
    [Route("api/recipe")]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get([FromRoute] int id)
        {
            var time = await _recipeService.GetAsync(id);
            return Ok(time);
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var recipes = await _recipeService.BrowseAsync();
            return Ok(recipes);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateRecipeDto dto)
        {
            var recipeId = await _recipeService.CreateAsync(dto);
            return Created($"api/recipe/{recipeId}", null);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _recipeService.DeleteAsync(id);
            return NoContent();
        }
    }
}
