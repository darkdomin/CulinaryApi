using CulinaryApi.Infrastructure.DTO.Times;
using CulinaryApi.Infrastructure.Services.Times;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CulinaryApi.Controllers
{
    [Route("api/recipe/time")]
    [Authorize(Roles = "Admin")]
    public class TimeController : ControllerBase
    {
        private readonly ITimeService _timeService;

        public TimeController(ITimeService timeService)
        {
            _timeService = timeService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get([FromRoute] int id)
        {
            var time = await _timeService.GetAsync(id);
            return Ok(time);
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var times = await _timeService.BrowseAsync();
            return Ok(times);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateTimeDto dto)
        {
            var timeId = await _timeService.CreateAsync(dto);
            return Created($"api/recipe/time/{timeId}", null);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromBody] UpdateTimeDto dto, int id)
        {
            await _timeService.UpdateAsync(dto, id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _timeService.DeleteAsync(id);
            return NoContent();
        }
    }
}
