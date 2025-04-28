using BookingSystem.Application.DTO;
using BookingSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        // GET: api/booking
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetAllBookings()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            return Ok(bookings);
        }

        // GET: api/booking/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDto>> GetBookingById(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
           // if (booking == null) return NotFound();
            return Ok(booking);
        }

        // POST: api/booking
        [HttpPost]
        public async Task<ActionResult> CreateBooking([FromBody] CreateBookingDto dto)
        {
            var booking = await _bookingService.CreateBookingAsync(dto);
            return CreatedAtAction(nameof(GetBookingById), new { id = booking.BookingId }, booking);
        }

        // PUT: api/booking/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBooking(int bookingId, [FromBody] CreateBookingDto dto)
        {
            var result = await _bookingService.UpdateBookingAsync(bookingId, dto);
            if (!result) return NotFound();
            return NoContent();
        }

        // DELETE: api/booking/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBooking(int bookingId)
        {
            var result = await _bookingService.DeleteBookingAsync(bookingId);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
