using BookingSystem.Application.Queries.User.GetUsersById;
using BookingSystem.Domain.Entities;
using BookingSystem.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Test.TestUserClass;

[TestFixture]
public class GetUsersByIdQueryHandlerTests
{
    private AppDbContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new AppDbContext(options);

        context.Roles.Add(new Role { RoleId = 1, RoleName = "Admin" });
        context.Users.Add(new User
        {
            UserId = 1,
            Fullname = "TestData",
            Email = "test@example.com",
            PasswordHash = "hash",
            RoleId = 1
        });

        context.SaveChanges();
        return context;
    }

    [TestCase(1, true)]
    [TestCase(99, false)]
    public async Task Handle_UserId_ReturnsExpectedResult(int userId, bool shouldExist)
    {
        using var context = CreateInMemoryContext();
        var handler = new GetUsersByIdQueryHandler(context);

        var result = await handler.Handle(new GetUsersByIdQuery(userId), CancellationToken.None);

        Assert.That(result.Success, Is.EqualTo(shouldExist));
    }
}

