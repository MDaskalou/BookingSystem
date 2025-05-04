using System.Threading;
using System.Threading.Tasks;
using BookingSystem.Application.Common;
using BookingSystem.Application.Service.Implementation;
using BookingSystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Application.Commands.UserCommands.LogInUser;

public class LogInUserCommandHandler : IRequestHandler<LogInUserCommand, OperationResult<string>>
{
    private readonly AppDbContext _context;
    private readonly AuthService _authService;

    public LogInUserCommandHandler(AppDbContext context, AuthService authService)
    {
        _context = context;
        _authService = authService;
    }

    public async Task<OperationResult<string>> Handle(LogInUserCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;

        var user = await _context.Users.Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == dto.Email, cancellationToken);

        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            return OperationResult<string>.Fail("Ogiltig inloggning");

        var token = _authService.GenerateJwtToken(user.UserId, user.Fullname, user.Role.RoleName);

        return OperationResult<string>.Ok(token, "Inloggning lyckades");
    }
}