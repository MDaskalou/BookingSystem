    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BookingSystem.Domain.Entities;

    namespace BookingSystem.Infrastructure.IRepository
    {
        public interface IPatientRepository
        {
            Task<IEnumerable<Patient>> GetAllAsync();
            Task<Patient?> GetByIdAsync(int id);
            Task AddAsync(Patient patient);
            Task UpdateAsync(Patient patient);
            Task DeleteAsync(Patient patient);
        }
    }
