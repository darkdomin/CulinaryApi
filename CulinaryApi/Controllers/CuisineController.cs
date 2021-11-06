using CulinaryApi.Core.Entieties;
using CulinaryApi.Infrastructure.DTO.Cuisines;
using CulinaryApi.Infrastructure.Services.Cuisines;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryApi.Controllers
{
    [Route("api/recipe/cuisine")]
    public class CuisineController : ControllerBase
    {
        private readonly ICuisineService _cuisineService;

        public CuisineController(ICuisineService cuisineService)
        {
            _cuisineService = cuisineService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get([FromRoute] int id)
        {
            var time = await _cuisineService.GetAsync(id);
            return Ok(time);
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var cuisines = await _cuisineService.BrowseAsync();
            return Ok(cuisines);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateCuisineDto dto)
        {
            var cuisineId = await _cuisineService.CreateAsync(dto);
            return Created($"api/recipe/cuisine/{cuisineId}", null);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _cuisineService.DeleteAsync(id);
            return NoContent();
        }
    }
}
