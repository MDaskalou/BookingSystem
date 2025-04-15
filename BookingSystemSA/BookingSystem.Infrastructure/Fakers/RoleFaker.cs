using Bogus;
using BookingSystem.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
namespace BookingSystem.Infrastructure.Fakers
{
    public class RoleFaker
    {
        public Faker<Role> GenerateRole()
        {

            var roleNames = new List<string>
            {
                "Admin",
                "Sjuksköterska",
                "ECTSjuksköterska",
                "Överläkare",
                "Underläkare",
                "Specialistläkare"
            };

            // Skapa en sequens som kommer att iterera över definitonerna
            var faker = new Faker<Role>()
                .RuleFor(r => r.RoleId, f => f.IndexFaker + 1) // Säkerställer unika ID:n 1-6
                .RuleFor(r => r.RoleName, (f, r) => roleNames[f.IndexFaker % roleNames.Count])
                .RuleFor(r => r.Users, f => new List<User>());


            return faker;
        }
    }
}