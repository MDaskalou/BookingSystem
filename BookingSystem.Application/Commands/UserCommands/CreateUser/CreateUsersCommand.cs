using BookingSystem.Application.Common;
using BookingSystem.Application.DTO;
using MediatR;

namespace BookingSystem.Application.Commands.UserCommands.CreateUser;

public record CreateUserCommand(CreateUserDto Dto) : IRequest<OperationResult<int>>;
