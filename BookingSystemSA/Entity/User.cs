using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookingSystemSA.Entity
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
        public Role role { get; set; } = null!;
        public int RoleId { get; set; } 
    }
}
