using BookingSystem.Application.DTO;

namespace BookingSystem.Application.Services
{
    public interface ITreatmentTypeService
    {
        Task<TreatmentTypeDto> CreateTreatmentTypeAsync(CreateTreatmentTypeDto dto);
        Task<TreatmentTypeDto?> GetTreatmentTypeByIdAsync(int id);
        Task<IEnumerable<TreatmentTypeDto>> GetAllTreatmentTypesAsync();
        Task<bool> UpdateTreatmentTypeAsync(int id, CreateTreatmentTypeDto dto);
        Task<bool> DeleteTreatmentTypeAsync(int id);
    }
}
