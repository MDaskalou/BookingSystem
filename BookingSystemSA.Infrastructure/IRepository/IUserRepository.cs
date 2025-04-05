using BookingSystem.Domain.Entities;

namespace BookingSystem.Infrastructure.IRepository
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<User?> GetByIdAsync(int userId);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByEmailAsync(string email);  // För att hämta användare via email
        Task UpdateAsync(User user);  // Uppdatera användare
        Task DeleteAsync(User user);  // Ta bort användare
    }
}
