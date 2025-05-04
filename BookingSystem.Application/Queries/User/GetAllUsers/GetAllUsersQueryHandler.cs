using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookingSystem.Application.DTO;
using BookingSystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Application.Queries.GetAllUsers;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
{
    private readonly AppDbContext _context;

    public GetAllUsersQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Include(u => u.Role)
            .Select(u => new UserDto
            {
                UserId = u.UserId,
                Fullname = u.Fullname,
                Email = u.Email,
                RoleName = u.Role.RoleName
            })
            .ToListAsync(cancellationToken);
    }
}
