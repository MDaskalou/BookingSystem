using BookingSystem.Application.DTO;
using MediatR;

namespace BookingSystem.Application.Commands.User.CreateUser;

public record CreateUserCommand (CreateUserDto Dto) : IRequest<int>;