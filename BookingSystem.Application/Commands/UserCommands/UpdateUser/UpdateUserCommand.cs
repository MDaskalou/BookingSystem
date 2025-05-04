using BookingSystem.Application.DTO;
using MediatR;

namespace BookingSystem.Application.Commands.UserCommands.UpdateUser;

public record UpdateUserCommand(int UserId, UpdateUserDto Dto) : IRequest<bool>;
