using BookingSystem.Application.DTO;
using BookingSystem.Domain.Entities;
using BookingSystem.Infrastructure.IRepository;
using BookingSystem.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using BookingSystem.Application.Service.Interface;
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
                UploadedByUserId = dto.UploadedById

            };

            await _repository.AddAsync(document);

            return new DocumentDto
            {
                DocumentId = document.DocumentId,
                FileName = document.FileName,
                Verified = document.Verified,
                UploadedById = document.UploadedByUserId

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
                UploadedById = document.UploadedByUserId

            };
        }

        public async Task<IEnumerable<DocumentDto>> GetAllDocumentsAsync(string? filter, string? sort)
        {
            var query = _context.Documents.AsQueryable();

            //Sorterar filnamn om ett sorteringsalternativ anges
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(d => d.FileName.Contains(filter));
            }

            if(!string.IsNullOrEmpty(sort))
            {
               if(sort.Equals("Ascending", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.OrderBy(d => d.FileName);
                }
                else if (sort.Equals("Descending", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.OrderByDescending(d => d.FileName);
                }
            }

            var documents = await _repository.GetAllAsync();
            return documents.Select(d => new DocumentDto
            {
                DocumentId = d.DocumentId,
                FileName = d.FileName,
                Verified = d.Verified,
                UploadedById = d.UploadedByUserId

            });
        }

        public async Task<bool> UpdateDocumentAsync(int id, CreateDocumentDto dto)
        {
            var document = await _repository.GetByIdAsync(id);
            if (document == null) return false;

            document.FileName = dto.FileName;
            document.Verified = dto.Verified;
            document.UploadedByUserId = dto.UploadedById;


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
