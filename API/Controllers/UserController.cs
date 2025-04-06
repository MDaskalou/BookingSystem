using BookingSystem.Application.DTO;
using BookingSystem.Application.Services;
using BookingSystem.Application.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/user/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(int userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null) return NotFound();
            return Ok(user);
        }

        // GET: api/user
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        // POST: api/user/register
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] CreateUserDto dto)
        {
            try
            {
                var user = await _userService.RegisterAsync(dto);
                return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  // Om användaren redan finns, returnera ett fel
            }
        }

        // PUT: api/user/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int userId, [FromBody] CreateUserDto dto)
        {
            var result = await _userService.UpdateUserAsync(userId, dto);
            if (!result) return NotFound();
            return NoContent();
        }

        // DELETE: api/user/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int userId)
        {
            var result = await _userService.DeleteUserAsync(userId);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
