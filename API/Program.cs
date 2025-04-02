using BookingSystem.Application.DTO;
using BookingSystem.Application.Service.Interface;
using BookingSystem.Application.Services;
using BookingSystem.Domain.Entities;
using BookingSystem.Infrastructure;
using BookingSystem.Infrastructure.DataBase;
using BookingSystem.Infrastructure.IRepository;
using BookingSystem.Infrastructure.Repositories;
using BookingSystem.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // --- ARC Configuration section ---
            // Set up DbContext
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add services (repositories, services, etc.)
            builder.Services.AddScoped<IPatientRepository, PatientRepository>();
            builder.Services.AddScoped<IPatientService, PatientService>();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();

            builder.Services.AddScoped<ITreatmentTypeService, TreatmentTypeService>();
            builder.Services.AddScoped<ITreatmentTypeRepository, TreatmentTypeRepository>();

            builder.Services.AddScoped<IDocumentService, DocumentService>();
            builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();

            // Lägg till andra tjänster
            builder.Services.AddScoped<DatabaseSeeder>();


            // Add controllers, Swagger, and other necessary services
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Kör seeding vid uppstart

            using (var scope = app.Services.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
                seeder.SeedData();
            }
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Middleware
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
