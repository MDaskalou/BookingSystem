using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
