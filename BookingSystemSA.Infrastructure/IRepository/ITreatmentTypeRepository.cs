using BookingSystem.Domain.Entities;

namespace BookingSystem.Infrastructure.IRepository
{
    public interface ITreatmentTypeRepository
    {
        Task AddAsync(TreatmentType treatmentType);
        Task<TreatmentType?> GetByIdAsync(int treatmentTypeId);
        Task<IEnumerable<TreatmentType>> GetAllAsync();
        Task UpdateAsync(TreatmentType treatmentType);
        Task DeleteAsync(TreatmentType treatmentType);
    }
}
