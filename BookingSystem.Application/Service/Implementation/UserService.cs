using BookingSystem.Application.DTO;
using BookingSystem.Application.Service;
using BookingSystem.Application.Service.Interface;
using BookingSystem.Domain.Entities;
using BookingSystem.Infrastructure.IRepository;
using BookingSystem.Infrastructure.Repositories;

namespace BookingSystem.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserDto> RegisterAsync(CreateUserDto dto)
        {
            // Kontrollera om användaren redan finns baserat på e-post
            var existingUser = await _repository.GetByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                throw new Exception("User already exists with this email.");
            }

            var user = new User
            {
                Fullname = dto.Fullname,
                Email = dto.Email,
                PasswordHash = dto.PasswordHash  // För enkelhetens skull, använd här en hashad version av lösenordet.
            };

            // Lägg till användaren i databasen
            await _repository.AddAsync(user);

            // Returnera en UserDto med användarens information
            return new UserDto
            {
                UserId = user.UserId,
                Fullname = user.Fullname,
                Email = user.Email
            };
        }

        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null) return null;

            return new UserDto
            {
                UserId = user.UserId,
                Fullname = user.Fullname,
                Email = user.Email
            };
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _repository.GetAllAsync();
            return users.Select(u => new UserDto
            {
                UserId = u.UserId,
                Fullname = u.Fullname,
                Email = u.Email
            });
        }

        public async Task<bool> UpdateUserAsync(int id, CreateUserDto dto)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null) return false;
            user.Fullname = dto.Fullname;
            user.Email = dto.Email;
            user.PasswordHash = dto.PasswordHash;
            await _repository.UpdateAsync(user);
            return true;
        }
        
        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null) return false;
            await _repository.DeleteAsync(user);
            return true;
        }
    }
}
