using BookingSystem.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Application.Service.Interface
{
    public interface INotificationService
    {
        Task<NotificationDto> CreateNotificationAsync(CreateNotificationDto dto);
        Task<NotificationDto?> GetNotificationByIdAsync(int id);
        Task<IEnumerable<NotificationDto>> GetAllNotificationsAsync();
        Task<bool> UpdateNotificationAsync(int id, CreateNotificationDto dto);
        Task<bool> DeleteNotificationAsync(int id);
    }
}
