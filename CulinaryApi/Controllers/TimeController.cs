using CulinaryApi.Core.Entieties;
using CulinaryApi.Infrastructure.DTO.FilterDto;
using CulinaryApi.Infrastructure.Services.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CulinaryApi.Controllers
{
    [Route("api/recipes/times")]
    [ApiController]
    public class TimeController : ControllerBase
    {
        private readonly IFilterService<Time> _timeService;

        public TimeController(IFilterService<Time> timeService)
        {
            _timeService = timeService;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult> Get([FromRoute] int id)
        {
            var time = await _timeService.GetAsync(id);
            return Ok(time);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Get()
        {
            var times = await _timeService.BrowseAsync();
            return Ok(times);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create([FromBody] CreateFilterDto<Time> dto)
        {
            var timeId = await _timeService.CreateAsync(dto);
            return Created($"api/times/{timeId}", null);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Update([FromBody] UpdateFilterDto<Time> dto, int id)
        {
            await _timeService.UpdateAsync(dto, id);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _timeService.DeleteAsync(id);
            return NoContent();
        }
    }
}
