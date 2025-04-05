using BookingSystem.Application.DTO;
using BookingSystem.Domain.Entities;
using BookingSystem.Infrastructure.IRepository;
using BookingSystem.Infrastructure.Repositories;

namespace BookingSystem.Application.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _repository;

        public DocumentService(IDocumentRepository repository)
        {
            _repository = repository;
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

        public async Task<DocumentDto?> GetDocumentByIdAsync(int documentId)
        {
            var document = await _repository.GetByIdAsync(documentId);
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
            var documents = await _repository.GetAllAsync();
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
