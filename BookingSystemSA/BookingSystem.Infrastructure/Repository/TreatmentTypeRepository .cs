using BookingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookingSystem.Infrastructure.IRepository;

namespace BookingSystem.Infrastructure.Repositories
{
    public class TreatmentTypeRepository : ITreatmentTypeRepository
    {
        private readonly AppDbContext _context;

        public TreatmentTypeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TreatmentType treatmentType)
        {
            _context.TreatmentTypes.Add(treatmentType);
            await _context.SaveChangesAsync();
        }

        public async Task<TreatmentType?> GetByIdAsync(int id)
        {
            return await _context.TreatmentTypes.FindAsync(id);
        }

        public async Task<IEnumerable<TreatmentType>> GetAllAsync()
        {
            return await _context.TreatmentTypes.ToListAsync();
        }

        public async Task UpdateAsync(TreatmentType treatmentType)
        {
            _context.TreatmentTypes.Update(treatmentType);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TreatmentType treatmentType)
        {
            _context.TreatmentTypes.Remove(treatmentType);
            await _context.SaveChangesAsync();
        }
    }
}
