using BookingSystemSA.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace BookingSystemSA.Entity
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
       
        public int PatientId { get; set; }
        public Patient Patient { get; set; } = null!;

        public int TreatmentTypeId { get; set; }
        public TreatmentType TreatmentType { get; set; } = null!;

        public int CreatedById { get; set; }
        public User CreatedBy { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        [Required]
        public BookingPriority Priority { get; set; }
        [Required]
        public BookingStatus Status { get; set; }
    }

}
