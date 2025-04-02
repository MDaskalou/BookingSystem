using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Domain.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string Fullname { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        //navigation
        public Role Role  { get; set; } = null!;
        public int RoleId { get; set; }
    }
}
