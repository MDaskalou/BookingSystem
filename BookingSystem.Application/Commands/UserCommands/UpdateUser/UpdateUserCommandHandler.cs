using System.Threading;
using System.Threading.Tasks;
using BookingSystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Application.Commands.UserCommands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
{
    private readonly AppDbContext _context;

    public UpdateUserCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);
        if (user == null) return false;

        if (!string.IsNullOrWhiteSpace(request.Dto.Fullname))
            user.Fullname = request.Dto.Fullname;

        if (!string.IsNullOrWhiteSpace(request.Dto.Email))
            user.Email = request.Dto.Email;

        if (!string.IsNullOrWhiteSpace(request.Dto.Password))
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Dto.Password);

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

}