using System.Threading;
using System.Threading.Tasks;
using BookingSystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Application.Commands.RoleCommand.UpdateRole;

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, bool>
{
    private readonly AppDbContext _context;

    public UpdateRoleCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _context.Roles
            .FirstOrDefaultAsync(r => r.RoleId == request.Id, cancellationToken);

        if (role == null) return false;

        role.RoleName = request.Dto.RoleName;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}