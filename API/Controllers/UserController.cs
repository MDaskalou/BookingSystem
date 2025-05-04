using BookingSystem.Application.Commands.UserCommands.DeleteUser;
using BookingSystem.Application.Commands.UserCommands.UpdateUser;
using BookingSystem.Application.DTO;
using BookingSystem.Application.Queries.GetAllUsers;
using BookingSystem.Application.Queries.User.GetUsersById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController( IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/user/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById([FromRoute] int id)
        {
            var user = await _mediator.Send(new GetUsersByIdQuery(id));
           
            if (user == null) return NotFound();
            
            return Ok(user);
        }
        

        // GET: api/user
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            var users = await _mediator.Send(new GetAllUsersQuery());
            return Ok(users);
        }
        

        // PUT: api/user/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, [FromBody] UpdateUserDto dto)
        {
            var success = await _mediator.Send(new UpdateUserCommand(id, dto));
            if (!success) return NotFound();

            return NoContent(); // 204
        }

        // DELETE: api/user/5
        [Authorize(Policy = "OnlyAdmin")]

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var success = await _mediator.Send(new DeleteUserCommand(id));
            if (!success) return NotFound();

            return Ok(new { message = "User successfully deleted" });
        }
    }
}
