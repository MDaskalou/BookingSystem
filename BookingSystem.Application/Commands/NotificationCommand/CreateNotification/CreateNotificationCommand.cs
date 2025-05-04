using BookingSystem.Application.DTO;
using MediatR;

namespace BookingSystem.Application.Commands.NotificationCommand.CreateNotification;

public record CreateNotificationCommand(CreateNotificationDto Dto) : IRequest<NotificationDto>;