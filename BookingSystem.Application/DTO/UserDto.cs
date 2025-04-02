using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Application.DTO
{
    public class UserDto
    {
        public int UserId { get; set; } 
        public string Email { get; set; } = string.Empty;
        public string Fullname { get; set; } = string.Empty;
    }

    public class CreateUserDto
    {
        public string Fullname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}
