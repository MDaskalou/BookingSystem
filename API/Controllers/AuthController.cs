using BookingSystem.Application.DTO;
using BookingSystem.Domain.Entities;
using BookingSystem.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BookingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly AppDbContext _context;

        public AuthController(AuthService authService, AppDbContext context)
        {
            _authService = authService;
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            // Validera användare (här kan du använda din UserService för att verifiera användaren)
            var user = ValidateUser(loginDto.Email, loginDto.Password);
            if (user == null)
            {
                return Unauthorized("Invalid credentials");
            }

            // Skapa JWT-token
            var token = _authService.GenerateJwtToken(user.UserId, user.Fullname, user.Role?.RoleName);
            return Ok(new { Token = token });
        }

        private User ValidateUser(string email, string passwordHash)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email); // Hämta rollen

            if (user == null)
            {
                return null;
            }

            if (user.PasswordHash != passwordHash)
            {
                return null; //ogiltig kod
            }

            var role = _context.Roles.FirstOrDefault(r => r.RoleId == user.RoleId);
            user.Role = role;
            return user;
        }
    }
}
