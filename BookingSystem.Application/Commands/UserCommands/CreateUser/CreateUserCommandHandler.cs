using System;
using System.Threading;
using System.Threading.Tasks;
using BookingSystem.Application.Commands.User.CreateUser;
using BookingSystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Application.Commands.UserCommands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler <CreateUserCommand, int>
{
    private readonly AppDbContext _context;

    public CreateUserCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        
        // Kolla om användaren redan finns via e-post
        var exist = await _context.Users.AnyAsync(u => u.Email == dto.Email, cancellationToken);
        if (exist) throw new Exception("Email finns redan");

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

        return user.UserId;
    }
}