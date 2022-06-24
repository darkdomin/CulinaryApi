using CulinaryApi.Core.Entieties;
using CulinaryApi.Infrastructure.DTO.Recipes;
using CulinaryApi.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CulinaryApi.Controllers
{
    [Route("api/recipes")]
    [ApiController]
    [Authorize]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById([FromRoute] int id)
        {
            
            var recipe = await _recipeService.GetAsync(id);
            return Ok(recipe);
        }


        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] RecipeQuery query)
        {
            var recipes = await _recipeService.BrowseAsync(query);
            return Ok(recipes);
        }

        [HttpGet("home")]
        public async Task<ActionResult> GetHome([FromQuery] RecipeQuery query)
        {
            var recipes = await _recipeService.BrowseHomeAsync(query);
            return Ok(recipes);
        }


        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateRecipeDto dto)
        {
            var recipeId = await _recipeService.CreateAsync(dto);
          
            return Created($"api/recipes/{recipeId}", null);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromBody] UpdateRecipeDto dto, int id)
        {
            await _recipeService.UpdateAsync(dto, id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _recipeService.DeleteAsync(id);
            return NoContent();
        }
    }
}
