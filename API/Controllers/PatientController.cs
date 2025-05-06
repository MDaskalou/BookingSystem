using BookingSystem.Application.Commands.PatientCommand.CreatePatient;
using BookingSystem.Application.Commands.PatientCommand.DeletePatient;
using BookingSystem.Application.Commands.PatientCommand.UpdatePatient;
using BookingSystem.Application.DTO;
using BookingSystem.Application.Queries.QueriesPatient.GetAllPatient;
using BookingSystem.Application.Queries.QueriesPatient.GetPatientById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

// TODO: Lägga till operational Result
// TODO: Jag behöver även lägga till OP i Min Madiatr, handlers

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PatientsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var patients = await _mediator.Send(new GetAllPatientsQuery());
        return Ok(patients);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var patient = await _mediator.Send(new GetPatientByIdQuery(id));
        if (patient == null) return NotFound();
        return Ok(patient);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePatientDto dto)
    {
        var created = await _mediator.Send(new CreatePatientCommand(dto));
        return CreatedAtAction(nameof(GetById), new { id = created.PatientId }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreatePatientDto dto)
    {
        var success = await _mediator.Send(new UpdatePatientCommand(id, dto));
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _mediator.Send(new DeletePatientCommand(id));
        if (!success) return NotFound();
        return Ok(new { message = "Patient successfully deleted" });
    }
}