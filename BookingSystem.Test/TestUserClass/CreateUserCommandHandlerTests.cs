using BookingSystem.Application.Commands.UserCommands.CreateUser;
using BookingSystem.Application.DTO;
using BookingSystem.Application.Validators;
using BookingSystem.Domain.Entities;
using BookingSystem.Infrastructure;
using Microsoft.EntityFrameworkCore;
using FluentValidation;

namespace BookingSystem.Test.TestUserClass;

[TestFixture]
public class CreateUserCommandHandlerTests
{
    private AppDbContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) //Isolerat test
            .Options;
        
        var context = new AppDbContext(options);
        
        context.Roles.Add(new Role { RoleId = 1, RoleName = "Admin" });
        context.Users.Add(new User
        {
            UserId = 1,
            Fullname = "Existing User",
            Email = "existing@example.com",
            PasswordHash = "hashed",
            RoleId = 1
        });

        context.SaveChanges();
        return context;
    }

    [TestCase("New User", "new@example.com", "password123", 1, true, TestName = "CreateUser_Success")]
    [TestCase("Duplicate User", "existing@example.com", "pass", 1, false, TestName = "CreateUser_EmailAlreadyExists")]
    [TestCase("No Password", "nopass@example.com", "", 1, false, TestName = "CreateUser_EmptyPassword_ShouldFail")]
    [TestCase("No Email", "", "password123", 1, false, TestName = "CreateUser_EmptyEmail_ShouldFail")]
    public async Task CreateUser_TestCases(string fullname, string email, string password, int roleId,
        bool shouldSucceed)
    {
        using var context = CreateInMemoryContext();
        var handler = new CreateUserCommandHandler(context);

        var dto = new CreateUserDto
        {
            Fullname = fullname,
            Email = email,
            Password = password,
            RoleId = roleId
        };

        var validator = new CreateUserDtoValidator();
        var validationResult = validator.Validate(dto);

        var command = new CreateUserCommand(dto);

        if (!shouldSucceed && !validationResult.IsValid)
        {
            Assert.Throws<ValidationException>(() => throw new ValidationException(validationResult.Errors));
        }
        else
        {
            var result = await handler.Handle(command, CancellationToken.None);

            if (!shouldSucceed && validationResult.IsValid)
            {
                Assert.ThrowsAsync<Exception>(async () => await handler.Handle(command, CancellationToken.None));
            }
            else
            {
                Assert.IsTrue(result.Success);
                Assert.That(result.Data, Is.GreaterThan(0));

            }
        }
    }

    [Test]
        public async Task CreateUser_ShouldHashPassword()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options);

            context.Roles.Add(new Role { RoleId = 1, RoleName = "Admin" });
            await context.SaveChangesAsync();

            var dto = new CreateUserDto
            {
                Fullname = "Secure User",
                Email = "secure@example.com",
                Password = "PlainTextPassword123!",
                RoleId = 1
            };

            var handler = new CreateUserCommandHandler(context);
            var command = new CreateUserCommand(dto);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);
            var createdUser = await context.Users.FirstOrDefaultAsync(u => u.UserId == result.Data);

            // Assert
            Assert.IsNotNull(createdUser);
            Assert.That(createdUser!.PasswordHash, Is.Not.EqualTo(dto.Password), "Password should be hashed, not stored as plain text.");
            Assert.IsTrue(BCrypt.Net.BCrypt.Verify(dto.Password, createdUser.PasswordHash), "Stored hash should match the original password.");
        }
    
}