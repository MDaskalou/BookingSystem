using MediatR;

namespace BookingSystem.Application.Commands.BookingCommand.DeleteCommand;

public record DeleteBookingCommand(int Id) : IRequest<bool>;