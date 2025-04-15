using BookingSystem.Application.DTO;

namespace BookingSystem.Application.Services
{
    public interface IDocumentService
    {
        Task<DocumentDto> CreateDocumentAsync(CreateDocumentDto dto);
        Task<DocumentDto?> GetDocumentByIdAsync(int dokumentId);
        Task<IEnumerable<DocumentDto>> GetAllDocumentsAsync(); // Uppdaterad signatur
        Task<bool> UpdateDocumentAsync(int dokumentId, CreateDocumentDto dto);
        Task<bool> DeleteDocumentAsync(int dokumentId);
    }
}
