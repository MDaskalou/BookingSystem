using BookingSystem.Domain.Entities;

namespace BookingSystem.Infrastructure.IRepository
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<User?> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetAllAsync();
// I IUserRepository.cs
        Task<User?> GetByEmailAsync(string email);
        Task UpdateAsync(User user);  // Uppdatera användare
        Task DeleteAsync(User user);  // Ta bort användare
    }
}
