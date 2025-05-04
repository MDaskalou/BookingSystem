using BookingSystem.Application.DTO;
using MediatR;

namespace BookingSystem.Application.Commands.RoleCommand.CreateRole;

public record CreateRoleCommand(CreateRoleDto Dto) : IRequest<RoleDto>;