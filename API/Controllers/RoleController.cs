using BookingSystem.Application.DTO;
using BookingSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        // POST: api/role
        [HttpPost]
        public async Task<ActionResult> CreateRole(CreateRoleDto dto)
        {
            var role = await _roleService.CreateRoleAsync(dto);
            return CreatedAtAction(nameof(GetRoleById), new { id = role.RoleId }, role);
        }

        // GET: api/role/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDto>> GetRoleById(int id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            if (role == null) return NotFound();
            return Ok(role);
        }

        // GET: api/role
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetAllRoles()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Ok(roles);
        }

        // PUT: api/role/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRole(int id, CreateRoleDto dto)
        {
            var result = await _roleService.UpdateRoleAsync(id, dto);
            if (!result) return NotFound();
            return NoContent();
        }

        // DELETE: api/role/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRole(int id)
        {
            var result = await _roleService.DeleteRoleAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
