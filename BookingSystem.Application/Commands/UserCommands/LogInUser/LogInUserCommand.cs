using BookingSystem.Application.DTO;
using MediatR;

namespace BookingSystem.Application.Commands.UserCommands.LogInUser;

public record LogInUserCommand (LoginDto Dto) : IRequest<string>;