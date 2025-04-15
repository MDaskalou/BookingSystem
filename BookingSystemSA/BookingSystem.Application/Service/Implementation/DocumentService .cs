using BookingSystem.Application.DTO;
using BookingSystem.Domain.Entities;
using BookingSystem.Infrastructure.IRepository;
using BookingSystem.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using BookingSystem.Application.Service;
using BookingSystem.Infrastructure;

namespace BookingSystem.Application.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _repository;
        private readonly AppDbContext _context;

        public DocumentService(IDocumentRepository repository, AppDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<DocumentDto> CreateDocumentAsync(CreateDocumentDto dto)
        {
            var document = new Document
            {
                FileName = dto.FileName,
                Verified = dto.Verified,
                UploadedByUserId = dto.UploadedByUserId
            };

            await _repository.AddAsync(document);

            return new DocumentDto
            {
                DocumentId = document.DocumentId,
                FileName = document.FileName,
                Verified = document.Verified,
                UploadedByUserId = document.UploadedByUserId
            };
        }

        public async Task<DocumentDto?> GetDocumentByIdAsync(int dokumetnId)
        {
            var document = await _repository.GetByIdAsync(dokumetnId);
            if (document == null) return null;

            return new DocumentDto
            {
                DocumentId = document.DocumentId,
                FileName = document.FileName,
                Verified = document.Verified,
                UploadedByUserId = document.UploadedByUserId
            };
        }

        public async Task<IEnumerable<DocumentDto>> GetAllDocumentsAsync()
        {
            // Hämta alla dokument utan filtrering eller sortering
            var documents = await _context.Documents.ToListAsync();
            return documents.Select(d => new DocumentDto
            {
                DocumentId = d.DocumentId,
                FileName = d.FileName,
                Verified = d.Verified,
                UploadedByUserId = d.UploadedByUserId
            });
        }


        public async Task<bool> UpdateDocumentAsync(int id, CreateDocumentDto dto)
        {
            var document = await _repository.GetByIdAsync(id);
            if (document == null) return false;

            document.FileName = dto.FileName;
            document.Verified = dto.Verified;
            document.UploadedByUserId = dto.UploadedByUserId;

            await _repository.UpdateAsync(document);
            return true;
        }

        public async Task<bool> DeleteDocumentAsync(int id)
        {
            var document = await _repository.GetByIdAsync(id);
            if (document == null) return false;

            await _repository.DeleteAsync(document);
            return true;
        }
    }
}
