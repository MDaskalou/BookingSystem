using System.Threading;
using System.Threading.Tasks;
using BookingSystem.Application.DTO;
using BookingSystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Application.Queries.QueriesNotification.GetNotificationById;

public class GetNotificationByIdQueryHandler : IRequestHandler<GetNotificationByIdQuery, NotificationDto?>
{
    private readonly AppDbContext _context;

    public GetNotificationByIdQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<NotificationDto?> Handle(GetNotificationByIdQuery request, CancellationToken cancellationToken)
    {
        var n = await _context.Notifications.FirstOrDefaultAsync(n => n.NotificationId == request.Id, cancellationToken);
        if (n == null) return null;

        return new NotificationDto
        {
            NotificationId = n.NotificationId,
            Message = n.Message,
            SentAt = n.SentAt,
            RecipientId = n.RecipientId
        };
    }
}