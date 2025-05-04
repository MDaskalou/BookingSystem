using System;
using BookingSystem.Domain.Enums;

namespace BookingSystem.Application.DTO
{
    public class BookingDto
    {
        public int BookingId { get; set; }
        public DateTime Date { get; set; }
        public int PatientId { get; set; }
        public int TreatmentTypeId { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedAt { get; set; }
        public BookingPriority Priority { get; set; }
        public BookingStatus Status { get; set; }
    }

    public class CreateBookingDto
    {
        public DateTime Date { get; set; }
        public int PatientId { get; set; }
        public int TreatmentTypeId { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedAt { get; set; }
        public BookingPriority Priority { get; set; }
        public BookingStatus Status { get; set; }
    }
}
