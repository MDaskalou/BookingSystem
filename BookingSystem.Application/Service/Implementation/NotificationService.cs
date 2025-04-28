using BookingSystem.Application.DTO;
using BookingSystem.Application.Service.Interface;
using BookingSystem.Domain.Entities;
using BookingSystem.Infrastructure.IRepository;

namespace BookingSystem.Application.Service.Implementation
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _repository;

        public NotificationService(INotificationRepository repository)
        {
            _repository = repository;
        }

        public async Task<NotificationDto> CreateNotificationAsync(CreateNotificationDto dto)
        {
            var notification = new Notification
            {
                Message = dto.Message,
                SentAt = DateTime.Now,
                RecipientId = dto.RecipientId
            };

            await _repository.AddAsync(notification);

            return new NotificationDto
            {
                NotificationId = notification.NotificationId,
                Message = notification.Message,
                SentAt = notification.SentAt,
                RecipientId = notification.RecipientId
            };
        }

        public async Task<NotificationDto?> GetNotificationByIdAsync(int id)
        {
            var notification = await _repository.GetByIdAsync(id);
            if (notification == null) return null;

            return new NotificationDto
            {
                NotificationId = notification.NotificationId,
                Message = notification.Message,
                SentAt = notification.SentAt,
                RecipientId = notification.RecipientId
            };
        }

        public async Task<IEnumerable<NotificationDto>> GetAllNotificationsAsync()
        {
            var notifications = await _repository.GetAllAsync();
            return notifications.Select(n => new NotificationDto
            {
                NotificationId = n.NotificationId,
                Message = n.Message,
                SentAt = n.SentAt,
                RecipientId = n.RecipientId
            });
        }

        public async Task<bool> UpdateNotificationAsync(int id, CreateNotificationDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            existing.Message = dto.Message;
            existing.RecipientId = dto.RecipientId;
            existing.SentAt = DateTime.Now;

            await _repository.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteNotificationAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            await _repository.DeleteAsync(existing);
            return true;
        }
    }
}

