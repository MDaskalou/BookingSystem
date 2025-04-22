using BookingSystem.Application.Service.Interface;
using BookingSystem.Application.Services;
using BookingSystem.Application.DTO;
using BookingSystem.Infrastructure.IRepository;
using BookingSystem.Domain.Entities;

namespace BookingSystem.Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repository;

        public PatientService(IPatientRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PatientDto>> GetAllAsync()
        {
            var patients = await _repository.GetAllAsync();

            return patients.Select(p => new PatientDto
            {
                PatientId = p.PatientId,
                FullName = p.FullName,
                SocialSecurityNumber = p.SocialSecurityNumber
            });
        }

        public async Task<PatientDto?> GetByIdAsync(int id)
        {
            var patient = await _repository.GetByIdAsync(id);
            if (patient == null) return null;

            return new PatientDto
            {
                PatientId = patient.PatientId,
                FullName = patient.FullName,
                SocialSecurityNumber = patient.SocialSecurityNumber
            };
        }

        public async Task<PatientDto> CreateAsync(CreatePatientDto dto)
        {
            var patient = new Patient
            {
                FullName = dto.FullName,
                SocialSecurityNumber = dto.SocialSecurityNumber
            };

            await _repository.AddAsync(patient);

            return new PatientDto
            {
                PatientId = patient.PatientId,
                FullName = patient.FullName,
                SocialSecurityNumber = patient.SocialSecurityNumber
            };
        }

        public async Task<bool> UpdateAsync(int id, CreatePatientDto dto)
        {
            var patient = await _repository.GetByIdAsync(id);
            if (patient == null) return false;

            patient.FullName = dto.FullName;
            patient.SocialSecurityNumber = dto.SocialSecurityNumber;

            await _repository.UpdateAsync(patient);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var patient = await _repository.GetByIdAsync(id);
            if (patient == null) return false;

            await _repository.DeleteAsync(patient);
            return true;
        }
    }
}
