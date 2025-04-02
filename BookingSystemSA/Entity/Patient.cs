using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookingSystemSA.Entity
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }
        [Required]
        public string FullName { get; set; } = string.Empty;
        [Required]
        public string SocialSecurityNumber { get; set; } = string.Empty;

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
