using MediatR;

namespace BookingSystem.Application.Commands.NotificationCommand.DeleteNotification;

public record DeleteNotificationCommand(int Id) : IRequest<bool>;