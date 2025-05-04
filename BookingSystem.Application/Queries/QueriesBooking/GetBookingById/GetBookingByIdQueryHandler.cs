using System.Threading;
using System.Threading.Tasks;
using BookingSystem.Application.DTO;
using BookingSystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Application.Queries.QueriesBooking.GetBookingById;

public class GetBookingByIdQueryHandler : IRequestHandler<GetBookingByIdQuery, BookingDto?>
{
    private readonly AppDbContext _context;

    public GetBookingByIdQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<BookingDto?> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
    {
        var b = await _context.Bookings
            .FirstOrDefaultAsync(b => b.BookingId == request.Id, cancellationToken);

        if (b == null) return null;

        return new BookingDto
        {
            BookingId = b.BookingId,
            Date = b.Date,
            PatientId = b.PatientId,
            TreatmentTypeId = b.TreatmentTypeId,
            CreatedById = b.CreatedByUserId,
            CreatedAt = b.CreatedAt,
            Priority = b.Priority,
            Status = b.Status
        };
    }

   
}