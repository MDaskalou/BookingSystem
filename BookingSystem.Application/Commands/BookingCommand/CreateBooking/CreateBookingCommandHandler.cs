using System.Threading;
using System.Threading.Tasks;
using BookingSystem.Application.DTO;
using BookingSystem.Domain.Entities;
using BookingSystem.Infrastructure;
using MediatR;

namespace BookingSystem.Application.Commands.BookingCommand.CreateBooking;

public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, BookingDto>
{
    private readonly AppDbContext _context;

    public CreateBookingCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<BookingDto> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;

        var booking = new Booking
        {
            Date = dto.Date,
            PatientId = dto.PatientId,
            TreatmentTypeId = dto.TreatmentTypeId,
            CreatedByUserId = dto.CreatedById,
            CreatedAt = dto.CreatedAt,
            Priority = dto.Priority,
            Status = dto.Status
        };

        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync(cancellationToken);

        return new BookingDto
        {
            BookingId = booking.BookingId,
            Date = booking.Date,
            PatientId = booking.PatientId,
            TreatmentTypeId = booking.TreatmentTypeId,
            CreatedById = booking.CreatedByUserId,
            CreatedAt = booking.CreatedAt,
            Priority = booking.Priority,
            Status = booking.Status
        };
    }
}