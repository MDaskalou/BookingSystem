using BookingSystem.Application.Commands.BookingCommand.CreateBooking;
using BookingSystem.Application.Commands.BookingCommand.DeleteCommand;
using BookingSystem.Application.Commands.BookingCommand.UpdateBooking;
using BookingSystem.Application.DTO;
using BookingSystem.Application.Queries.QueriesBooking.GetAllBookings;
using BookingSystem.Application.Queries.QueriesBooking.GetBookingById;
using BookingSystem.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookingController(  IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/booking
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetAllBookings()
        {
            var bookings = await _mediator.Send(new GetAllBookingsQuery());
            return Ok(bookings);
        }


        // GET: api/booking/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDto>> GetBookingById(int id)
        {
            var booking = await _mediator.Send(new GetBookingByIdQuery(id));
            if (booking == null) return NotFound();

            return Ok(booking);
        }

        // POST: api/booking
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingDto dto)
        {
            var created = await _mediator.Send(new CreateBookingCommand(dto));
            return CreatedAtAction(nameof(GetBookingById), new { id = created.BookingId }, created);
        }


        // PUT: api/booking/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] CreateBookingDto dto)
        {
            var success = await _mediator.Send(new UpdateBookingCommand(id, dto));
            if (!success) return NotFound();

            return NoContent();
        }

        

        // DELETE: api/booking/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var result = await _mediator.Send(new DeleteBookingCommand(id));
            if (!result) return NotFound();

            return Ok(new { message = "Booking successfully deleted" });
        }

    }
}
