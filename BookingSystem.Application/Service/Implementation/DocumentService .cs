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
                UploadedById = dto.UploadedById
            };

            await _repository.AddAsync(document);

            return new DocumentDto
            {
                DocumentId = document.DocumentId,
                FileName = document.FileName,
                Verified = document.Verified,
                UploadedById = document.UploadedById
            };
        }

        public async Task<DocumentDto?> GetDocumentByIdAsync(int id)
        {
            var document = await _repository.GetByIdAsync(id);
            if (document == null) return null;

            return new DocumentDto
            {
                DocumentId = document.DocumentId,
                FileName = document.FileName,
                Verified = document.Verified,
                UploadedById = document.UploadedById
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
                UploadedById = d.UploadedById
            });
        }

        public async Task<bool> UpdateDocumentAsync(int id, CreateDocumentDto dto)
        {
            var document = await _repository.GetByIdAsync(id);
            if (document == null) return false;

            document.FileName = dto.FileName;
            document.Verified = dto.Verified;
            document.UploadedById = dto.UploadedById;

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
