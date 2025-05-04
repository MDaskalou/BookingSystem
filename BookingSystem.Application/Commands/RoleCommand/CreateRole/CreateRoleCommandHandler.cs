using System.Threading;
using System.Threading.Tasks;
using BookingSystem.Application.DTO;
using BookingSystem.Domain.Entities;
using BookingSystem.Infrastructure;
using MediatR;

namespace BookingSystem.Application.Commands.RoleCommand.CreateRole;

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, RoleDto>
{
    private readonly AppDbContext _context;

    public CreateRoleCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<RoleDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var entity = new Role
        {
            RoleName = request.Dto.RoleName
        };

        _context.Roles.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return new RoleDto
        {
            RoleId = entity.RoleId,
            RoleName = entity.RoleName
        };
    }
}