using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookingSystem.Application.DTO;
using BookingSystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Application.Queries.QueriesNotification.GetAllNotifications;

public class GetAllNotificationsQueryHandler : IRequestHandler<GetAllNotificationsQuery, IEnumerable<NotificationDto>>
{
    private readonly AppDbContext _context;

    public GetAllNotificationsQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<NotificationDto>> Handle(GetAllNotificationsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Notifications
            .Select(n => new NotificationDto
            {
                NotificationId = n.NotificationId,
                Message = n.Message,
                SentAt = n.SentAt,
                RecipientId = n.RecipientId
            })
            .ToListAsync(cancellationToken);
    }
}