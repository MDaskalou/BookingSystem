using System;
using System.Threading;
using System.Threading.Tasks;
using BookingSystem.Application.Service.Implementation;
using BookingSystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Application.Commands.UserCommands.LogInUser;

public class LogInUserCommandHandler : IRequestHandler<LogInUserCommand, string>
{
    private readonly AppDbContext _context;
    private readonly AuthService _authService;

    public LogInUserCommandHandler(AppDbContext context, AuthService authService)
    {
        _context = context;
        _authService = authService;
    }

    public async Task<string> Handle(LogInUserCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        
        var user = await _context.Users.Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == dto.Email, cancellationToken);

        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            throw new Exception("Ogiltig Inlogning");
        
        return _authService.GenerateJwtToken(user.UserId, user.Fullname, user.Role.RoleName);
    }
}