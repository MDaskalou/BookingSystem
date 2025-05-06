using BookingSystem.Application.Commands.DocumentCommand.CreateDocument;
using BookingSystem.Application.Commands.DocumentCommand.DeleteDocument;
using BookingSystem.Application.Commands.DocumentCommand.UpdateDocument;
using BookingSystem.Application.DTO;
using BookingSystem.Application.Queries.QueriesDocument.GetAllDocuments;
using BookingSystem.Application.Queries.QueriesDocument.GetDocumentById;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class DocumentController : ControllerBase
{
    private readonly IMediator _mediator;

    public DocumentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // POST: api/document
    [HttpPost]
    public async Task<IActionResult> CreateDocument([FromBody] CreateDocumentDto dto)
    {
        var result = await _mediator.Send(new CreateDocumentCommand(dto));
        if (!result.Success) return BadRequest(result.ErrorMessage);
        
        return CreatedAtAction(nameof(GetDocumentById), new { id = result.Data!.DocumentId }, result.Data);
        
    }

    // GET: api/document/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetDocumentById(int id)
    {
        var result = await _mediator.Send(new GetDocumentByIdQuery(id));
        if (!result.Success)
            return NotFound(result.ErrorMessage);;
        return Ok(result.Data);
    }

    // GET: api/document
    [HttpGet]
    public async Task<IActionResult> GetAllDocuments()
    {
        var result = await _mediator.Send(new GetAllDocumentsQuery());
        if (!result.Success)
            return BadRequest(result.ErrorMessage);

        return Ok(result.Data);
    }

    // PUT: api/document/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDocument(int id, [FromBody] CreateDocumentDto dto)
    {
        var result = await _mediator.Send(new UpdateDocumentCommand(id, dto));
        if (!result.Success)
            return NotFound(result.ErrorMessage);
        
        return NoContent();
    }

    // DELETE: api/document/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDocument(int id)
    {
        var result = await _mediator.Send(new DeleteDocumentCommand(id));
        if (!result.Success) return NotFound();
        return NotFound(result.ErrorMessage);
        
        return Ok(new { message = result.Message });
    }
}
