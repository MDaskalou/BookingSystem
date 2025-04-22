
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

            //  Global filter för soft delete på User
            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);
            
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.CreatedBy)                         // navigation property
                .WithMany()                                       // en användare kan ha flera bokningar
                .HasForeignKey(b => b.CreatedByUserId)            // detta är FK i Booking
                .OnDelete(DeleteBehavior.Restrict);               // förhindra att användare tas bort automatiskt med bokning
            
            modelBuilder.Entity<Patient>().ToTable("Patients");


            base.OnModelCreating(modelBuilder);
        }        
    }
}
