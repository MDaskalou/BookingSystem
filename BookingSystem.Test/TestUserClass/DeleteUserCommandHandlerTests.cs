using BookingSystem.Application.Commands.UserCommands.DeleteUser;
using BookingSystem.Application.Users.Commands.DeleteUser;
using BookingSystem.Domain.Entities;
using BookingSystem.Infrastructure;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace BookingSystem.Test.TestUserClass;

[TestFixture]
public class DeleteUserCommandHandlerTests
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
            Fullname = "User One",
            Email = "user1@example.com",
            PasswordHash = "hashed",
            RoleId = 1
        });
        context.Users.Add(new User
        {
            UserId = 2,
            Fullname = "User Two",
            Email = "user2@example.com",
            PasswordHash = "hashed",
            RoleId = 1,
            IsDeleted = true
        });

        context.SaveChanges();
        return context;
    }

    [Test]
    public async Task DeleteUser_ExistingUser_ShouldMarkAsDeleted()
    {
        using var context = CreateInMemoryContext();
        var handler = new DeleteUserCommandHandler(context);

        var result = await handler.Handle(new DeleteUserCommand(1), CancellationToken.None);
        var user = await context.Users.FindAsync(1);

        Assert.IsTrue(result);
        Assert.IsTrue(user!.IsDeleted);
    }

    [Test]
    public async Task DeleteUser_NonExistentUser_ShouldReturnFalse()
    {
        using var context = CreateInMemoryContext();
        var handler = new DeleteUserCommandHandler(context);

        var result = await handler.Handle(new DeleteUserCommand(99), CancellationToken.None);
        Assert.IsFalse(result);
    }

    [Test]
    public async Task DeleteUser_AlreadyDeletedUser_ShouldReturnFalse()
    {
        using var context = CreateInMemoryContext();
        var handler = new DeleteUserCommandHandler(context);

        var result = await handler.Handle(new DeleteUserCommand(2), CancellationToken.None);
        Assert.IsFalse(result);
    }

    [Test]
    public async Task DeleteUser_CheckSoftDeleteOnly()
    {
        using var context = CreateInMemoryContext();
        var handler = new DeleteUserCommandHandler(context);

        await handler.Handle(new DeleteUserCommand(1), CancellationToken.None);
        var user = await context.Users.IgnoreQueryFilters().FirstOrDefaultAsync(u => u.UserId == 1);

        Assert.IsNotNull(user);
        Assert.IsTrue(user!.IsDeleted);
        Assert.That(user.Email, Is.EqualTo("user1@example.com")); // Kontrollera att annan data finns kvar
    }
}
