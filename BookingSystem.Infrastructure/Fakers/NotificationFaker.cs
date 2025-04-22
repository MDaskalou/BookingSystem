using Bogus;
using BookingSystem.Domain.Entities;

namespace BookingSystem.Infrastructure.Fakers
{
    public class NotificationFaker
    {
        public Faker<Notification> GenerateNotification(IEnumerable<User> users)
        {
            var faker = new Faker<Notification>()
                .RuleFor(n => n.Message, f => f.Lorem.Sentence()) // Slumpmässigt meddelande
                .RuleFor(n => n.SentAt, f => f.Date.Recent()) // Slumpmässigt datum
                .RuleFor(n => n.RecipientId, f => f.PickRandom(users).UserId); // Tilldela en slumpmässig RecipientId

            return faker;
        }
    }
}
