using BookingSystem.Application.DTO;

namespace BookingSystem.Application.Services
{
    public interface IDocumentService
    {
        Task<DocumentDto> CreateDocumentAsync(CreateDocumentDto dto);
        Task<DocumentDto?> GetDocumentByIdAsync(int id);
        Task<IEnumerable<DocumentDto>> GetAllDocumentsAsync();
        Task<bool> UpdateDocumentAsync(int id, CreateDocumentDto dto);
        Task<bool> DeleteDocumentAsync(int id);
    }
}
