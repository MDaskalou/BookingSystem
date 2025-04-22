using BookingSystem.Application.DTO;
using BookingSystem.Domain.Entities;
using BookingSystem.Infrastructure.IRepository;
using BookingSystem.Infrastructure.Repositories;

namespace BookingSystem.Application.Services
{
    public class TreatmentTypeService : ITreatmentTypeService
    {
        private readonly ITreatmentTypeRepository _repository;

        public TreatmentTypeService(ITreatmentTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<TreatmentTypeDto> CreateTreatmentTypeAsync(CreateTreatmentTypeDto dto)
        {
            var treatmentType = new TreatmentType
            {
                Name = dto.Name
            };

            await _repository.AddAsync(treatmentType);

            return new TreatmentTypeDto
            {
                TreatmentTypeId = treatmentType.TreatmentTypeId,
                Name = treatmentType.Name
            };
        }

        public async Task<TreatmentTypeDto?> GetTreatmentTypeByIdAsync(int id)
        {
            var treatmentType = await _repository.GetByIdAsync(id);
            if (treatmentType == null) return null;

            return new TreatmentTypeDto
            {
                TreatmentTypeId = treatmentType.TreatmentTypeId,
                Name = treatmentType.Name
            };
        }

        public async Task<IEnumerable<TreatmentTypeDto>> GetAllTreatmentTypesAsync()
        {
            var treatmentTypes = await _repository.GetAllAsync();
            return treatmentTypes.Select(tt => new TreatmentTypeDto
            {
                TreatmentTypeId = tt.TreatmentTypeId,
                Name = tt.Name
            });
        }

        public async Task<bool> UpdateTreatmentTypeAsync(int id, CreateTreatmentTypeDto dto)
        {
            var treatmentType = await _repository.GetByIdAsync(id);
            if (treatmentType == null) return false;

            treatmentType.Name = dto.Name;

            await _repository.UpdateAsync(treatmentType);
            return true;
        }

        public async Task<bool> DeleteTreatmentTypeAsync(int id)
        {
            var treatmentType = await _repository.GetByIdAsync(id);
            if (treatmentType == null) return false;

            await _repository.DeleteAsync(treatmentType);
            return true;
        }
    }
}
