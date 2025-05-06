using System.Threading;
using System.Threading.Tasks;
using Azure;
using BookingSystem.Application.Common;
using BookingSystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Application.Commands.BookingCommand.UpdateBooking;

public class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand,OperationResult<bool>>
{
    private readonly AppDbContext _context;

    public UpdateBookingCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<OperationResult<bool>> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
    {
        var booking = await _context.Bookings
            .FirstOrDefaultAsync(b => b.BookingId == request.Id, cancellationToken);

        if (booking == null)
            return OperationResult<bool>.Fail("Booking not found");

        var dto = request.Dto;

        booking.Date = dto.Date;
        booking.PatientId = dto.PatientId;
        booking.TreatmentTypeId = dto.TreatmentTypeId;
        booking.CreatedByUserId = dto.CreatedById;
        booking.CreatedAt = dto.CreatedAt;
        booking.Priority = dto.Priority;
        booking.Status = dto.Status;

        await _context.SaveChangesAsync(cancellationToken);
        return OperationResult<bool>.Ok(true);
    }
}