using System.Collections.Generic;
using System.Threading.Tasks;
using BookingSystem.Application.DTO;

namespace BookingSystem.Application.Services
{
    public interface IUserService
    {
        Task<UserDto?> GetUserByIdAsync(int userId);
        Task<bool> UpdateUserAsync(int userId, UpdateUserDto dto);
        Task<bool> DeleteUserAsync(int userId);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
         
    }
}
