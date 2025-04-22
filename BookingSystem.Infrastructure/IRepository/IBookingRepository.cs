using BookingSystem.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingSystem.Infrastructure.IRepository
{
    public interface IBookingRepository
    {
        Task<Booking> GetByIdAsync(int bookingId);
        Task<IEnumerable<Booking>> GetAllAsync();
        Task AddAsync(Booking booking);
        Task UpdateAsync(Booking booking);
        Task DeleteAsync(Booking booking);
    }
}
