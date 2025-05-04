using System.Threading;
using System.Threading.Tasks;
using BookingSystem.Application.DTO;
using BookingSystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Application.Queries.QueriesRole.GetRolesById;

public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, RoleDto?>
{
    private readonly AppDbContext _context;

    public GetRoleByIdQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<RoleDto?> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await _context.Roles
            .FirstOrDefaultAsync(r => r.RoleId == request.Id, cancellationToken);

        if (role == null) return null;

        return new RoleDto
        {
            RoleId = role.RoleId,
            RoleName = role.RoleName
        };
    }
}