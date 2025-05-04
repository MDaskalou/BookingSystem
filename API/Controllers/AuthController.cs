using BookingSystem.Application.Commands.UserCommands.CreateUser;
using BookingSystem.Application.Commands.UserCommands.LogInUser;
using BookingSystem.Application.Common;
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

        [HttpPost("login")]
        public async Task<ActionResult<OperationResult<string>>> Login([FromBody] LoginDto dto)
        {
            var result = await _mediator.Send(new LogInUserCommand(dto));

            if (!result.Success)
                return Unauthorized(result);

            return Ok(result);
        }

        [Authorize(Policy = "OnlyAdmin")]
        [HttpPost("register")]
        public async Task<ActionResult<OperationResult<int>>> Register([FromBody] CreateUserDto dto)
        {
            var result = await _mediator.Send(new CreateUserCommand(dto));

            if (!result.Success)
                return BadRequest(result);

            // Alternativt: return Created(...) med OperationResult inuti
            return Ok(result);
        }
    }
}