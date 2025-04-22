using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingSystem.Application.DTO;

namespace BookingSystem.Application.Service.Interface
{
    public interface IPatientService
    {
        Task<IEnumerable<PatientDto>> GetAllAsync();
        Task<PatientDto?> GetByIdAsync(int id);
        Task<PatientDto> CreateAsync(CreatePatientDto dto);
        Task<bool> UpdateAsync(int id, CreatePatientDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
