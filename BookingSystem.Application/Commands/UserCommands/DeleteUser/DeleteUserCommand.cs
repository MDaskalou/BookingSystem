using MediatR;

namespace BookingSystem.Application.Commands.UserCommands.DeleteUser;


public record DeleteUserCommand(int UserId) : IRequest<bool>;
