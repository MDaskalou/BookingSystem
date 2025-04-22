using Bogus;
using BookingSystem.Domain.Entities;

namespace BookingSystem.Infrastructure.Fakers
{
    public class PatientFaker
    {
        public Faker<Patient> GeneratePatient()
        {
            var faker = new Faker<Patient>()
                //.RuleFor(p => p.PatientId, f => f.IndexFaker) // Generera ett unikt PatientId (baserat på Faker index)
                .RuleFor(p => p.FullName, f => f.Name.FullName()) // Generera ett realistiskt namn
                .RuleFor(p => p.SocialSecurityNumber, (f, p) => f.Random.Int(100000000, 999999999).ToString())
                .RuleFor(p => p.Bookings, f => new List<Booking>()); // Tom lista för Bookings, kan fyllas om det behövs

            return faker;
        }
    }
}
