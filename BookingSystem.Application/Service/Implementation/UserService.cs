using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSystem.Application.DTO;
using BookingSystem.Application.Services;
using BookingSystem.Domain.Entities;
using BookingSystem.Infrastructure;
using BookingSystem.Infrastructure.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace BookingSystem.Application.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly AppDbContext _context;

        public UserService(IUserRepository repository, AppDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _repository = repository;
            _context = context;
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
