using BookingSystem.Application.DTO;
using MediatR;

namespace BookingSystem.Application.Queries.QueriesNotification.GetNotificationById;

public record GetNotificationByIdQuery(int Id) : IRequest<NotificationDto?>;