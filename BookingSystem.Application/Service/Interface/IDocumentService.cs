using BookingSystem.Application.DTO;

namespace BookingSystem.Application.Services
{
    public interface IDocumentService
    {
        Task<DocumentDto> CreateDocumentAsync(CreateDocumentDto dto);
        Task<DocumentDto?> GetDocumentByIdAsync(int id);
        Task<IEnumerable<DocumentDto>> GetAllDocumentsAsync(string? filter, string? sort); // Uppdaterad signatur
        Task<bool> UpdateDocumentAsync(int id, CreateDocumentDto dto);
        Task<bool> DeleteDocumentAsync(int id);
    }
}
