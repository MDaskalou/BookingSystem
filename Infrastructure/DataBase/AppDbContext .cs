using Microsoft.EntityFrameworkCore;
using BookingSystemSA.Entity;
namespace Infrastructure.DataBase
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<Booking> Bookings => Set<Booking>();
        public DbSet<TreatmentType> TreatmentTypes => Set<TreatmentType>();
        public DbSet<Document> Documents => Set<Document>();
        public DbSet<Notification> Notifications => Set<Notification>();
    }
}
