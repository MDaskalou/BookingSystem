using Bogus;
using BookingSystem.Domain.Entities;

namespace BookingSystemSA.Infrastructure.Fakers
{
    public class RoleFaker
    {
        public Faker<Role> GenerateRole()
        {
            var faker = new Faker<Role>()
                .RuleFor(r => r.RoleName, f => f.PickRandom("Admin", "User", "Manager"));

            return faker;
        }
    }
}
