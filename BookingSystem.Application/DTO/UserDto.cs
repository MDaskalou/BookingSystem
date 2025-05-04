
namespace BookingSystem.Application.DTO
{
    public class UserDto
    {
        public int UserId { get; set; } 
        public string Email { get; set; } = string.Empty;
        public string Fullname { get; set; } = string.Empty;
        public string? RoleName { get; set; } = string.Empty;
    }

    public class CreateUserDto
    {
        public string Fullname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int RoleId { get; set; }
    }

    public class UpdateUserDto
    {
        public string? Fullname { get; set; } 
        public string? Email { get; set; } 
        public string? Password { get; set; } 
    }
}
