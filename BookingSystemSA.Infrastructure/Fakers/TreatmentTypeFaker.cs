using Bogus;
using BookingSystem.Domain.Entities;

namespace BookingSystemSA.Infrastructure.Fakers
{
    public class TreatmentTypeFaker
    {
        public Faker<TreatmentType> GenerateTreatmentType()
        {
            var faker = new Faker<TreatmentType>()
                .RuleFor(tt => tt.Name, f => f.PickRandom("ECT", "rTMS", "Maintenance ECT", "Outpatient Index Series"));

            return faker;
        }
    }
}
