using BookingSystem.Application.DTO;
using MediatR;
using System.Collections.Generic;

namespace BookingSystem.Application.Roles.Queries.GetAllRoles
{
    public record GetAllRolesQuery() : IRequest<IEnumerable<RoleDto>>;
}