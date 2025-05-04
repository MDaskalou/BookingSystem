using BookingSystem.Application.Commands.User.CreateUser;
using BookingSystem.Application.Commands.UserCommands.LogInUser;
using BookingSystem.Application.DTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        //Detta är en controller för autentisering och registrering av användare. Den innehåller metoder för att logga in och registrera användare,
        //samt hämta användarinformation baserat på ID.

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            try
            {
                var token = await _mediator.Send(new LogInUserCommand(dto));
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });

            }
        }

        // Denna metod används för att logga in en användare.
        // Den tar emot en LoginDto som innehåller e-post och lösenord.
        [Authorize(Policy = "OnlyAdmin")]

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDto dto)
        {
            try
            {
                var userId = await _mediator.Send(new CreateUserCommand(dto));

                return CreatedAtAction(
                    actionName: "GetUserById",
                    controllerName: "User",
                    routeValues: new { id = userId },
                    value: userId
                );
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }


}
