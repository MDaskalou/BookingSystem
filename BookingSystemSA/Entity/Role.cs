﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookingSystemSA.Entity
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        [Required]
        public string RoleName { get; set; } = string.Empty;
        public ICollection<User> Users { get; set; }
    }

}
