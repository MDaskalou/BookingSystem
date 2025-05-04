using BookingSystem.Application.Commands.RoleCommand.CreateRole;
using BookingSystem.Application.Commands.RoleCommand.DeleteRole;
using BookingSystem.Application.Commands.RoleCommand.UpdateRole;
using BookingSystem.Application.DTO;
using BookingSystem.Application.Queries.QueriesRole.GetRolesById;
using BookingSystem.Application.Roles.Queries.GetAllRoles;
using BookingSystem.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IMediator _mediator;

        public RoleController(IRoleService roleService, IMediator mediator)
        {
            _roleService = roleService;
            _mediator = mediator;
        }

        // POST: api/role
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDto dto)
        {
            var created = await _mediator.Send(new CreateRoleCommand(dto));
            return CreatedAtAction("GetRoleById", new { id = created.RoleId }, created);
        }


        // GET: api/role/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDto>> GetRoleById(int id)
        {
            var role = await _mediator.Send(new GetRoleByIdQuery(id));
            if (role == null) return NotFound();
            return Ok(role);
        }


        // GET: api/role
        [HttpGet]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetAllRoles()
        {
            var roles = await _mediator.Send(new GetAllRolesQuery());
            return Ok(roles);
        }

        // PUT: api/role/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] CreateRoleDto dto)
        {
            var updated = await _mediator.Send(new UpdateRoleCommand(id, dto));
            if (!updated) return NotFound();

            return NoContent();
        }


        // DELETE: api/role/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var deleted = await _mediator.Send(new DeleteRoleCommand(id));
            if (!deleted) return NotFound();

            return Ok(new { message = "Role successfully deleted" });
        }

    }
}
