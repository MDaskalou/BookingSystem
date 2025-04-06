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
        public string Email { get; set; } 
        public string Fullname { get; set; }
        public string RoleName { get; set; }
    }

    public class CreateUserDto
    {
        public string Fullname { get; set; } 
        public string Email { get; set; } 
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}
