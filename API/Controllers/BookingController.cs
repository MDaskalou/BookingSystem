using BookingSystem.Application.Commands.BookingCommand.CreateBooking;
using BookingSystem.Application.Commands.BookingCommand.DeleteCommand;
using BookingSystem.Application.Commands.BookingCommand.UpdateBooking;
using BookingSystem.Application.DTO;
using BookingSystem.Application.Queries.QueriesBooking.GetAllBookings;
using BookingSystem.Application.Queries.QueriesBooking.GetBookingById;
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
            var result = await _mediator.Send(new GetAllBookingsQuery());
            if(!result.Success)
                return BadRequest(result.ErrorMessage);
            
            return Ok(result.Data);
        }


        // GET: api/booking/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDto>> GetBookingById(int id)
        {
            var result = await _mediator.Send(new GetBookingByIdQuery(id));
            if(!result.Success)
                return NotFound(result.ErrorMessage);

            return Ok(result.Data);
        }

        // POST: api/booking
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingDto dto)
        {
            var result = await _mediator.Send(new CreateBookingCommand(dto));
            
            return CreatedAtAction(nameof(GetBookingById), new { id = result.Data!.BookingId }, result.Data);
        }


        // PUT: api/booking/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] CreateBookingDto dto)
        {
            var result = await _mediator.Send(new UpdateBookingCommand(id, dto));
            if (!result.Success)
                return NotFound(result.ErrorMessage);

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
