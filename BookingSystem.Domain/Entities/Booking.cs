using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingSystem.Domain.Enums;


namespace BookingSystem.Domain.Entities
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]

        public int PatientId { get; set; }
        public Patient Patient { get; set; } 

        public int TreatmentTypeId { get; set; }
        public TreatmentType TreatmentType { get; set; } 

        public int CreatedByUserId { get; set; }
        public User CreatedBy { get; set; } 

        public DateTime CreatedAt { get; set; }

        [Required]
        public BookingPriority Priority { get; set; }
        [Required]
        public BookingStatus Status { get; set; }
    }
}
