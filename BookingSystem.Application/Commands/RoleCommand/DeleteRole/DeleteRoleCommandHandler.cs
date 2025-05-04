using System.Threading;
using System.Threading.Tasks;
using BookingSystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Application.Commands.RoleCommand.DeleteRole;

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, bool>
{
    private readonly AppDbContext _context;

    public DeleteRoleCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _context.Roles
            .FirstOrDefaultAsync(r => r.RoleId == request.Id, cancellationToken);

        if (role == null) return false;

        _context.Roles.Remove(role);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}