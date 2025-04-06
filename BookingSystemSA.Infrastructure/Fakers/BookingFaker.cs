using Bogus;
using BookingSystem.Domain.Entities;
using BookingSystem.Domain.Enums;

namespace BookingSystem.Infrastructure.Fakers
{
    public class BookingFaker
    {
        public Faker<Booking> GenerateBooking(IEnumerable<Patient> patients, IEnumerable<TreatmentType> treatmentTypes, IEnumerable<User> users)
        {
            var faker = new Faker<Booking>()
                //.RuleFor(b => b.BookingId, f => f.Random.Int(1, 1000)) // Random BookingId
                .RuleFor(b => b.Date, f => f.Date.Future()) // Random date in the future
                .RuleFor(b => b.PatientId, f => f.PickRandom(patients).PatientId) // Random patient
                .RuleFor(b => b.TreatmentTypeId, f => f.PickRandom(treatmentTypes).TreatmentTypeId) // Random treatment type
                .RuleFor(b => b.CreatedById, f => f.PickRandom(users).UserId) // Random user (creator)
                .RuleFor(b => b.CreatedAt, f => f.Date.Recent()) // Recent created date
                .RuleFor(b => b.Priority, f => f.PickRandom<BookingPriority>()) // Random priority from enum
                .RuleFor(b => b.Status, f => f.PickRandom<BookingStatus>()); // Random status from enum

            return faker;
        }
    }
}
