using CulinaryApi.Core.Entieties;
using CulinaryApi.Infrastructure.DTO;
using CulinaryApi.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
