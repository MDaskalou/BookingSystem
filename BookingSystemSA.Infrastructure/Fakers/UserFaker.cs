using Bogus;
using BookingSystem.Domain.Entities;

namespace BookingSystemSA.Infrastructure.Fakers
{
    public class UserFaker
    {
        public Faker<User> GenerateUser()
        {
            var faker = new Faker<User>()
                .RuleFor(u => u.Fullname, f => f.Name.FullName())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.PasswordHash, f => f.Internet.Password())
                .RuleFor(u => u.RoleId, f => f.PickRandom(1, 2, 3));  // Välj en roll (ex. 1 = Admin, 2 = User, 3 = Manager)

            return faker;
        }
    }
}
