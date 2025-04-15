using System.Threading.Tasks;
using BookingSystem.Application.DTO;
using BookingSystem.Domain.Entities;
using BookingSystem.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly AppDbContext _context;
        private readonly IPasswordHasher <User> _passwordHasher;

        public AuthController(AuthService authService, AppDbContext context, PasswordHasher<User> passwordHasher)
        {
            _authService = authService;
            _context = context;
            _passwordHasher = passwordHasher;
        }
        //Detta är en controller för autentisering och registrering av användare. Den innehåller metoder för att logga in och registrera användare,
        //samt hämta användarinformation baserat på ID.

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            // Hämta användaren med inkluderad Role
            var user = await _context.Users.Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null)
            {
                return Unauthorized("Invalid credentials");
            }

            // Verifiera lösenordet med IPasswordHasher
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);
            if (result != PasswordVerificationResult.Success)
            {
                return Unauthorized("Invalid credentials");
            }

            // Generera JWT-token
            var token = _authService.GenerateJwtToken(user.UserId, user.Fullname, user.Role?.RoleName);
            return Ok(new { Token = token });
        }
        // Denna metod används för att logga in en användare.
        // Den tar emot en LoginDto som innehåller e-post och lösenord.
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDto dto)
        {

            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (existingUser != null)
            {
                return BadRequest("User with this email already exists.");
            }
            // Validera och registrera användare
            var user = new User
            {
                Fullname = dto.Fullname,
                Email = dto.Email,
                RoleId = dto.RoleId
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password); // Hasha lösenordet

            // Spara användaren i databasen
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var userDto = new UserDto
            {
                UserId = user.UserId,
                Fullname = user.Fullname,
                Email = user.Email,
                RoleName = user.Role?.RoleName
            };


            return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, userDto);
        }

        // Getuserbyid är en metod som används för att hämta en användare baserat på deras ID.
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _context.Users.Include(u => u.Role)
                                           .FirstOrDefaultAsync(u => u.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            var userDto = new UserDto
            {
                UserId = user.UserId,
                Fullname = user.Fullname,
                Email = user.Email,
                RoleName = user.Role?.RoleName
            };
            return Ok(userDto);
        }

        private async Task<User> ValidateUserAsync(string email, string password)
        {
            var user = await _context.Users.Include(u => u.Role)
                                            .FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return null;
            }
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return result == PasswordVerificationResult.Success ? user : null;
        }
    }
}
