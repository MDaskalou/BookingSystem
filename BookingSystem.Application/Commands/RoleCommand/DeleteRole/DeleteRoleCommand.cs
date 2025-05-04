using MediatR;

namespace BookingSystem.Application.Commands.RoleCommand.DeleteRole;

public record DeleteRoleCommand(int Id) : IRequest<bool>;