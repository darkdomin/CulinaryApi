using CulinaryApi.Infrastructure.DTO.Users;
using CulinaryApi.Infrastructure.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CulinaryApi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.Get();
            return Ok(users);
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync([FromBody]RegisterUserDto dto)
        {
            await _userService.RegisterAsync(dto);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync([FromBody] LoginDto dto) 
        {
            var token =  await _userService.LoginAsync(dto);
            return Ok(token);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "LimitedAccess")]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _userService.RemoveAsync(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize(Policy= "LimitedAccess")]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult> Rename([FromRoute] int id,[FromBody] UserUpdateDto dto)
        {
            await _userService.UpdateAsync(dto, id);
            return Ok();
        }

        //[HttpPut]
        //[Authorize(Roles = "Admin")]
        //public async Task<ActionResult> ChangeRole(UserUpdateRole dto)
        //{
        //    await _userService.ChangeRole(dto);
        //    return Ok();
        //}
    }
}
