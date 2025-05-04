using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookingSystem.Application.DTO;
using BookingSystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Application.Queries.QueriesBooking.GetAllBookings;

public class GetAllBookingsQueryHandler : IRequestHandler<GetAllBookingsQuery, IEnumerable<BookingDto>>
{
    private readonly AppDbContext _context;

    public GetAllBookingsQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BookingDto>> Handle(GetAllBookingsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Bookings
            .Select(b => new BookingDto
            {
                BookingId = b.BookingId,
                Date = b.Date,
                PatientId = b.PatientId,
                TreatmentTypeId = b.TreatmentTypeId,
                CreatedById = b.CreatedByUserId,
                CreatedAt = b.CreatedAt,
                Priority = b.Priority,
                Status = b.Status
            })
            .ToListAsync(cancellationToken);
    }
}