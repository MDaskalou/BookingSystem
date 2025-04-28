using BookingSystem.Application.DTO;
using BookingSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        // POST: api/document
        [HttpPost]
        public async Task<ActionResult> CreateDocument(CreateDocumentDto dto)
        {
            var document = await _documentService.CreateDocumentAsync(dto);
            return CreatedAtAction(nameof(GetDocumentById), new { id = document.DocumentId }, document);
        }

        // GET: api/document/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentDto>> GetDocumentById(int dokumentId)
        {
            var document = await _documentService.GetDocumentByIdAsync(dokumentId);
            if (document == null) return NotFound();
            return Ok(document);
        }

        // GET: api/document
        [HttpGet("Get-All-Dokument")]
        public async Task<ActionResult<IEnumerable<DocumentDto>>> GetAllDocuments(int dokumentId)
        {
            var documents = await _documentService.GetAllDocumentsAsync();
            return Ok(documents);
        }

        // PUT: api/document/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDocument(int dokumentId, CreateDocumentDto dto)
        {
            var result = await _documentService.UpdateDocumentAsync(dokumentId, dto);
            if (!result) return NotFound();
            return NoContent();
        }

        // DELETE: api/document/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDocument(int dokumentId)
        {
            var result = await _documentService.DeleteDocumentAsync(dokumentId);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
