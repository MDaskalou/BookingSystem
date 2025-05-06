using BookingSystem.Application.Commands.TreatmentTypeCommands.CreateTreatmentType;
using BookingSystem.Application.Commands.TreatmentTypeCommands.DeleteTreatmentType;
using BookingSystem.Application.Commands.TreatmentTypeCommands.UpdateTreatmentType;
using BookingSystem.Application.DTO;
using BookingSystem.Application.Queries.QueriesTreatmentType.GetAllTreatmentType;
using BookingSystem.Application.Queries.QueriesTreatmentType.GetAllTreatmentTypeById;
using BookingSystem.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// TODO: Lägga till operational Result
// TODO: Jag behöver även lägga till OP i Min Madiatr, handlers

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TreatmentTypeController : ControllerBase
    {
        private readonly ITreatmentTypeService _treatmentTypeService;
        private readonly IMediator _mediator;

        public TreatmentTypeController(ITreatmentTypeService treatmentTypeService, IMediator mediator)
        {
            _treatmentTypeService = treatmentTypeService;
            _mediator = mediator;
        }

        // POST: api/treatmenttype
        [HttpPost]
        public async Task<IActionResult> CreateTreatmentType([FromBody] CreateTreatmentTypeDto dto)
        {
            var created = await _mediator.Send(new CreateTreatmentTypeCommand(dto));
            return CreatedAtAction("GetTreatmentTypeById", new { id = created.TreatmentTypeId }, created);
        }


        // GET: api/treatmenttype/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TreatmentTypeDto>> GetTreatmentTypeById(int id)
        {
            var treatmentType = await _mediator.Send(new GetTreatmentTypeByIdQuery(id));
            if (treatmentType == null) return NotFound();
            return Ok(treatmentType);
        }


        // GET: api/treatmenttype
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TreatmentTypeDto>>> GetAllTreatmentTypes()
        {
            var treatmentTypes = await _mediator.Send(new GetAllTreatmentTypesQuery());
            return Ok(treatmentTypes);
        }


        // PUT: api/treatmenttype/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTreatmentType(int id, [FromBody] CreateTreatmentTypeDto dto)
        {
            var success = await _mediator.Send(new UpdateTreatmentTypeCommand(id, dto));
            if (!success) return NotFound();

            return NoContent();
        }


        // DELETE: api/treatmenttype/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTreatmentType(int id)
        {
            var result = await _mediator.Send(new DeleteTreatmentTypeCommand(id));
            if (!result) return NotFound();

            return Ok(new { message = "Treatment type deleted" });
        }

    }
}
