using System.Threading;
using System.Threading.Tasks;
using BookingSystem.Application.Common;
using BookingSystem.Application.DTO;
using BookingSystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Application.Queries.User.GetUsersById;

public class GetUsersByIdQueryHandler : IRequestHandler<GetUsersByIdQuery, OperationResult<UserDto>>
{
    private readonly AppDbContext _context;

    public GetUsersByIdQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<OperationResult<UserDto>> Handle(GetUsersByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.UserId == request.Id, cancellationToken);

        if (user == null)
            return OperationResult<UserDto>.Fail("User not found");

        var userDto = new UserDto
        {
            UserId = user.UserId,
            Fullname = user.Fullname,
            Email = user.Email,
            RoleName = user.Role.RoleName
        };

        return OperationResult<UserDto>.Ok(userDto);
    }
}