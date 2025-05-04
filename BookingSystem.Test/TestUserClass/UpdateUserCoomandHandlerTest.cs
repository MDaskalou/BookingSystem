using BookingSystem.Application.Commands.UserCommands.UpdateUser;
using BookingSystem.Application.DTO;
using BookingSystem.Domain.Entities;
using BookingSystem.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Test.TestUserClass;

[TestFixture]
public class UpdateUserCommandHandlerTest
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
            Fullname = "Original Name",
            Email = "original@example.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("originalpassword"),
            RoleId = 1
        });
        context.SaveChanges();
        return context;
    }

    [Test]
    public async Task UpdateUser_ValidData_ShouldUpdateUser()
    {
        using var context = CreateInMemoryContext();
        var handler = new UpdateUserCommandHandler(context);
        var dto = new UpdateUserDto { Fullname = "Updated Name", Email = "updated@example.com", Password = "newpassword" };

        var command = new UpdateUserCommand(1, dto);
        var result = await handler.Handle(command, CancellationToken.None);

        var updatedUser = await context.Users.FindAsync(1);
        Assert.IsTrue(result);
        Assert.That(updatedUser!.Fullname, Is.EqualTo("Updated Name"));
        Assert.That(updatedUser.Email, Is.EqualTo("updated@example.com"));
        Assert.IsTrue(BCrypt.Net.BCrypt.Verify("newpassword", updatedUser.PasswordHash));
    }

    [Test]
    public async Task UpdateUser_NonExistentUser_ShouldReturnFalse()
    {
        using var context = CreateInMemoryContext();
        var handler = new UpdateUserCommandHandler(context);
        var dto = new UpdateUserDto { Fullname = "Doesn't Matter", Email = "doesntmatter@example.com", Password = "pwd" };

        var result = await handler.Handle(new UpdateUserCommand(99, dto), CancellationToken.None);
        Assert.IsFalse(result);
    }

    [Test]
    public async Task UpdateUser_OnlyName_ShouldNotChangeOtherFields()
    {
        using var context = CreateInMemoryContext();
        var handler = new UpdateUserCommandHandler(context);
        var dto = new UpdateUserDto { Fullname = "Only Name Changed", Email = "", Password = "" };

        var result = await handler.Handle(new UpdateUserCommand(1, dto), CancellationToken.None);
        var user = await context.Users.FindAsync(1);

        Assert.IsTrue(result);
        Assert.That(user!.Fullname, Is.EqualTo("Only Name Changed"));
        Assert.That(user.Email, Is.EqualTo("original@example.com"));
    }

    [Test]
    public async Task UpdateUser_EmptyPassword_ShouldNotChangeHash()
    {
        using var context = CreateInMemoryContext();
        var user = await context.Users.FindAsync(1);
        var originalHash = user!.PasswordHash;

        var handler = new UpdateUserCommandHandler(context);
        var dto = new UpdateUserDto { Fullname = "No Change", Email = "nochange@example.com", Password = "" };

        var result = await handler.Handle(new UpdateUserCommand(1, dto), CancellationToken.None);
        var updatedUser = await context.Users.FindAsync(1);

        Assert.IsTrue(result);
        Assert.That(updatedUser!.PasswordHash, Is.EqualTo(originalHash));
    }
}
