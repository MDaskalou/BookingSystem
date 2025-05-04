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
        var created = await _mediator.Send(new CreateDocumentCommand(dto));
        return CreatedAtAction(nameof(GetDocumentById), new { id = created.DocumentId }, created);
    }

    // GET: api/document/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetDocumentById(int id)
    {
        var document = await _mediator.Send(new GetDocumentByIdQuery(id));
        if (document == null) return NotFound();
        return Ok(document);
    }

    // GET: api/document
    [HttpGet]
    public async Task<IActionResult> GetAllDocuments()
    {
        var documents = await _mediator.Send(new GetAllDocumentsQuery());
        return Ok(documents);
    }

    // PUT: api/document/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDocument(int id, [FromBody] CreateDocumentDto dto)
    {
        var success = await _mediator.Send(new UpdateDocumentCommand(id, dto));
        if (!success) return NotFound();
        return NoContent();
    }

    // DELETE: api/document/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDocument(int id)
    {
        var success = await _mediator.Send(new DeleteDocumentCommand(id));
        if (!success) return NotFound();
        return Ok(new { message = "Document successfully deleted" });
    }
}
