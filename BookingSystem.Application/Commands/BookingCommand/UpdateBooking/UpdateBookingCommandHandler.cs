using System.Threading;
using System.Threading.Tasks;
using BookingSystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Application.Commands.BookingCommand.UpdateBooking;

public class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand, bool>
{
    private readonly AppDbContext _context;

    public UpdateBookingCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
    {
        var booking = await _context.Bookings
            .FirstOrDefaultAsync(b => b.BookingId == request.Id, cancellationToken);

        if (booking == null) return false;

        var dto = request.Dto;

        booking.Date = dto.Date;
        booking.PatientId = dto.PatientId;
        booking.TreatmentTypeId = dto.TreatmentTypeId;
        booking.CreatedByUserId = dto.CreatedById;
        booking.CreatedAt = dto.CreatedAt;
        booking.Priority = dto.Priority;
        booking.Status = dto.Status;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}