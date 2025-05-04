using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Domain.Entities
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }
        public string FullName { get; set; } 
        public string SocialSecurityNumber { get; set; } 

        public ICollection<Booking> Bookings { get; set; } 
    }
}
