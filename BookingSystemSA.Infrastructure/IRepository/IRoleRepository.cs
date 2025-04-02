using BookingSystem.Domain.Entities;

namespace BookingSystem.Infrastructure.IRepository
{
    public interface IRoleRepository
    {
        Task AddAsync(Role role);
        Task<Role?> GetByIdAsync(int id);
        Task<IEnumerable<Role>> GetAllAsync();
        Task UpdateAsync(Role role);
        Task DeleteAsync(Role role);
    }
}
