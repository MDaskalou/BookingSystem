using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSystem.Application.DTO;
using BookingSystem.Application.Services;
using BookingSystem.Domain.Entities;
using BookingSystem.Infrastructure.IRepository;

namespace BookingSystem.Application.Service.Implementation
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _repository;

        public BookingService(IBookingRepository repository)
        {
            _repository = repository;
        }

        public async Task<BookingDto> GetBookingByIdAsync(int id)
        {
            var booking = await _repository.GetByIdAsync(id);

            return new BookingDto
            {
                BookingId = booking.BookingId,
                Date = booking.Date,
                PatientId = booking.PatientId,
                TreatmentTypeId = booking.TreatmentTypeId,
                CreatedById = booking.CreatedByUserId,
                CreatedAt = booking.CreatedAt,
                Priority = booking.Priority,
                Status = booking.Status
            };
        }

        public async Task<IEnumerable<BookingDto>> GetAllBookingsAsync()
        {
            var bookings = await _repository.GetAllAsync();
            return bookings.Select(b => new BookingDto
            {
                BookingId = b.BookingId,
                Date = b.Date,
                PatientId = b.PatientId,
                TreatmentTypeId = b.TreatmentTypeId,
                CreatedById = b.CreatedByUserId,
                CreatedAt = b.CreatedAt,
                Priority = b.Priority,
                Status = b.Status
            });
        }

        public async Task<BookingDto> CreateBookingAsync(CreateBookingDto dto)
        {
            var booking = new Booking
            {
                Date = dto.Date,
                PatientId = dto.PatientId,
                TreatmentTypeId = dto.TreatmentTypeId,
                CreatedByUserId = dto.CreatedById,
                CreatedAt = dto.CreatedAt,
                Priority = dto.Priority,
                Status = dto.Status
            };

            await _repository.AddAsync(booking);
            return new BookingDto
            {
                BookingId = booking.BookingId,
                Date = booking.Date,
                PatientId = booking.PatientId,
                TreatmentTypeId = booking.TreatmentTypeId,
                CreatedById = booking.CreatedByUserId,
                CreatedAt = booking.CreatedAt,
                Priority = booking.Priority,
                Status = booking.Status
            };
        }

        public async Task<bool> UpdateBookingAsync(int id, CreateBookingDto dto)
        {
            var booking = await _repository.GetByIdAsync(id);

            booking.Date = dto.Date;
            booking.PatientId = dto.PatientId;
            booking.TreatmentTypeId = dto.TreatmentTypeId;
            booking.CreatedByUserId = dto.CreatedById;
            booking.CreatedAt = dto.CreatedAt;
            booking.Priority = dto.Priority;
            booking.Status = dto.Status;

            await _repository.UpdateAsync(booking);
            return true;
        }

        public async Task<bool> DeleteBookingAsync(int id)
        {
            var booking = await _repository.GetByIdAsync(id);

            await _repository.DeleteAsync(booking);
            return true;
        }
    }
}
