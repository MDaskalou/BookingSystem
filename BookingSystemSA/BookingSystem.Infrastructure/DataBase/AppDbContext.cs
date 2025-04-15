
using BookingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Document = BookingSystem.Domain.Entities.Document;

namespace BookingSystem.Infrastructure
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

     
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Undvik att skapa om tabellen 'Patients'
            modelBuilder.Entity<Patient>().ToTable("Patients", t => t.ExcludeFromMigrations());

            //  Global filter för soft delete på User
            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);

            base.OnModelCreating(modelBuilder);
        }        
    }
}
