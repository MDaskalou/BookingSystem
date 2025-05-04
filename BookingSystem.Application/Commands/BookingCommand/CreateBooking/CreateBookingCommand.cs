using BookingSystem.Application.DTO;
using MediatR;

namespace BookingSystem.Application.Commands.BookingCommand.CreateBooking;

public record CreateBookingCommand(CreateBookingDto Dto) : IRequest<BookingDto>;