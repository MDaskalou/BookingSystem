using BookingSystem.Application.Common;
using BookingSystem.Application.DTO;
using MediatR;

namespace BookingSystem.Application.Commands.BookingCommand.UpdateBooking;

public record UpdateBookingCommand(int Id, CreateBookingDto Dto) : IRequest<OperationResult<bool>>;