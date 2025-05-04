
using System.ComponentModel.DataAnnotations;


namespace BookingSystem.Domain.Entities
{
    public class TreatmentType
    {
        [Key]
        public int TreatmentTypeId { get; set; }
        [Required]
        public string Name { get; set; }  // ECT, rTMS, Maintenance ECT, etc.
        public ICollection<Booking> Bookings { get; set; } 
    }
}
