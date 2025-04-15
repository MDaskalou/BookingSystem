using BookingSystem.Application.DTO;
using BookingSystem.Application.Services;
using BookingSystem.Domain.Entities;
using BookingSystem.Infrastructure;
using BookingSystem.Infrastructure.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Application.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly AppDbContext _context;

        public UserService(IUserRepository repository, AppDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<UserDto> RegisterAsync(CreateUserDto dto)
        {
            // Kontrollera om användaren redan finns baserat på e-post
            var existingUser = await _repository.GetByEmailAsync(dto.Email);

            if (existingUser != null)
            {
                throw new Exception("User already exists with this email.");
            }

            // Skapa en ny användare
            var user = new User
            {
                Fullname = dto.Fullname,
                Email = dto.Email,
                PasswordHash = dto.Password,  // Obs! Hasha lösenordet här!
                RoleId = dto.RoleId // Tilldela en roll baserat på DTO
            };

            // Lägg till användaren i databasen
            await _repository.AddAsync(user);

            // Bekräfta att användaren har sparats i databasen
            await _context.SaveChangesAsync(); // Om detta krävs av din implementation

            // Hämta användaren från databasen med inkluderade roller (för säker matchning)
            var registeredUser = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            // Om av någon anledning användaren inte hittas
            if (registeredUser == null)
            {
                throw new Exception("Failed to save the user to the database.");
            }

            // Returnera en UserDto med användarens fullständiga information
            return new UserDto
            {
                UserId = registeredUser.UserId,
                Fullname = registeredUser.Fullname,
                Email = registeredUser.Email,
                RoleName = registeredUser.Role?.RoleName // Null check, om roles inte laddats korrekt
            };
        }


        public async Task<UserDto?> GetUserByIdAsync(int userId)
        {
            var user = await _repository.GetByIdAsync(userId);
            if (user == null)
            {
                return null;
            }

            var userDto = new UserDto
            {
                UserId = user.UserId,
                Fullname = user.Fullname,
                Email = user.Email,
                RoleName = user.Role?.RoleName
            };

            return userDto;

        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _repository.GetAllAsync();
            return users.Select(u => new UserDto
            {
                UserId = u.UserId,
                Fullname = u.Fullname,
                Email = u.Email,
                RoleName = u.Role?.RoleName 

            });
        }

        public async Task<bool> UpdateUserAsync(int userId, UpdateUserDto dto)
        {
            var user = await _repository.GetByIdAsync(userId);
            if (user == null) return false;
            
            //kollar om någon annan användaren har samma e-post
            var existingUser = await _repository.GetByEmailAsync(dto.Email);
            if (existingUser != null && existingUser.UserId != userId)
            {
                throw new Exception("User already exists with this email.");
            }
            
            user.Fullname = dto.Fullname;
            user.Email = dto.Email;
            user.PasswordHash = dto.Password;
            await _repository.UpdateAsync(user);
            return true;
        }
        
        public async Task<bool> DeleteUserAsync(int id)
        {
            // Kontrollera om användaren finns
            var user = await _repository.GetByIdAsync(id);
            // Om användaren inte finns, returnera false
            if (user == null) return false;
            // Ta bort användaren
            await _repository.DeleteAsync(user);
            // Returnera true för att indikera att borttagningen lyckades
            return true;
        }
    }
}
