using System.Threading;
using System.Threading.Tasks;
using BookingSystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Application.Commands.BookingCommand.DeleteCommand;

public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand, bool>
{
    private readonly AppDbContext _context;

    public DeleteBookingCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
    {
        var booking = await _context.Bookings
            .FirstOrDefaultAsync(b => b.BookingId == request.Id, cancellationToken);

        if (booking == null) return false;

        _context.Bookings.Remove(booking);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}