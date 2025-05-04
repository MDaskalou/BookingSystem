using System.Threading;
using System.Threading.Tasks;
using BookingSystem.Application.Common;
using BookingSystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Application.Commands.UserCommands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, OperationResult<int>>
{
    private readonly AppDbContext _context;

    public CreateUserCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<OperationResult<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;

        var exist = await _context.Users.AnyAsync(u => u.Email == dto.Email, cancellationToken);
        if (exist)
            return OperationResult<int>.Fail("Email finns redan");

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var user = new Domain.Entities.User
        {
            Fullname = dto.Fullname,
            Email = dto.Email,
            PasswordHash = hashedPassword,
            RoleId = dto.RoleId
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        return OperationResult<int>.Ok(user.UserId, "Användare skapad");
    }
}