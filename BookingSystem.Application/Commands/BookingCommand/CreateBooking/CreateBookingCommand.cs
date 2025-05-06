using BookingSystem.Application.Common;
using BookingSystem.Application.DTO;
using MediatR;

namespace BookingSystem.Application.Commands.BookingCommand.CreateBooking;

public class CreateBookingCommand : IRequest<OperationResult<BookingDto>>
{
    public CreateBookingDto Dto { get; }

    public CreateBookingCommand(CreateBookingDto dto)
    {
        Dto = dto;
    }
}