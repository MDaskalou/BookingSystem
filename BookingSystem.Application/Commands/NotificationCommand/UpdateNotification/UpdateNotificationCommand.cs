using BookingSystem.Application.DTO;
using MediatR;

namespace BookingSystem.Application.Commands.NotificationCommand.UpdateNotification;

public record UpdateNotificationCommand(int Id, CreateNotificationDto Dto) : IRequest<bool>;