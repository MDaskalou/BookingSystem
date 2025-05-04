using System;
using System.Threading;
using System.Threading.Tasks;
using BookingSystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Application.Commands.NotificationCommand.UpdateNotification;

public class UpdateNotificationCommandHandler : IRequestHandler<UpdateNotificationCommand, bool>
{
    private readonly AppDbContext _context;

    public UpdateNotificationCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateNotificationCommand request, CancellationToken cancellationToken)
    {
        var notification = await _context.Notifications
            .FirstOrDefaultAsync(n => n.NotificationId == request.Id, cancellationToken);

        if (notification == null) return false;

        notification.Message = request.Dto.Message;
        notification.RecipientId = request.Dto.RecipientId;
        notification.SentAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}