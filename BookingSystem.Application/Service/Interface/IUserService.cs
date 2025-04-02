using BookingSystem.Application.DTO;

namespace BookingSystem.Application.Services
{
    public interface IUserService
    {
        Task<UserDto> RegisterAsync(CreateUserDto dto);
        Task<UserDto?> GetUserByIdAsync(int id);
        Task<bool> UpdateUserAsync(int id, CreateUserDto dto);
        Task<bool> DeleteUserAsync(int id);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
    }
}
