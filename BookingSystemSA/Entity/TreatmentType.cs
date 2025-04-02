using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace BookingSystemSA.Entity
{
    public class TreatmentType
    {
        [Key]
        public int TreatmentTypeId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty; // ECT, rTMS, Maintenance ECT, etc.
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
