using BookingSystem.Application.Common;
using BookingSystem.Application.DTO;
using MediatR;

namespace BookingSystem.Application.Queries.User.GetUsersById;


public record GetUsersByIdQuery(int Id) : IRequest<Result<UserDto>>;
