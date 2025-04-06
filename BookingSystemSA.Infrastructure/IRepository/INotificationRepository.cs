using BookingSystem.Domain.Entities;

namespace BookingSystem.Infrastructure.IRepository
{
    public interface INotificationRepository
    {
        Task AddAsync(Notification notification);
        Task<Notification?> GetByIdAsync(int notificationId);
        Task<IEnumerable<Notification>> GetAllAsync();
        Task UpdateAsync(Notification notification);
        Task DeleteAsync(Notification notification);
    }
}
