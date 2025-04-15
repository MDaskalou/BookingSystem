using BookingSystem.Application.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingSystem.Application.Services
{
    public interface IBookingService
    {
        Task<BookingDto> GetBookingByIdAsync(int bookingId);
        Task<IEnumerable<BookingDto>> GetAllBookingsAsync();
        Task<BookingDto> CreateBookingAsync(CreateBookingDto dto);
        Task<bool> UpdateBookingAsync(int bookingId, CreateBookingDto dto);
        Task<bool> DeleteBookingAsync(int bookingId);
    }
}
