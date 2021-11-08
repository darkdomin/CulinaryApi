using CulinaryApi.Infrastructure.DTO.Difficulties;
using CulinaryApi.Infrastructure.Services.Difficulties;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryApi.Controllers
{
    [Route("api/recipe/difficulty")]
    public class DifficultyController : ControllerBase
    {
        private readonly IDifficultyService _difficultyService;

        public DifficultyController(IDifficultyService difficultyService)
        {
            _difficultyService = difficultyService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get([FromRoute] int id)
        {
            var time = await _difficultyService.GetAsync(id);
            return Ok(time);
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var difficulties = await _difficultyService.BrowseAsync();
            return Ok(difficulties);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateDifficultyDto dto)
        {
            var difficultyId = await _difficultyService.CreateAsync(dto);
            return Created($"api/recipe/difficulty/{difficultyId}", null);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromBody] UpdateDifficultyDto dto, int id)
        {
            await _difficultyService.UpdateAsync(dto, id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _difficultyService.DeleteAsync(id);
            return NoContent();
        }
    }
}
