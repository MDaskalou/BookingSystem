using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using BookingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
    }
}
