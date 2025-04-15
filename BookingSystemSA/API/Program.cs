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
using Microsoft.Extensions.Options;
using BookingSystem.API.ExceptionMiddleware;
using BookingSystem.Application.Service.Implementation;

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


            // Repositories
            builder.Services.AddScoped<IPatientRepository, PatientRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<ITreatmentTypeRepository, TreatmentTypeRepository>();
            builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
            builder.Services.AddScoped<IBookingRepository, BookingRepository>();
            builder.Services.AddScoped<INotificationRepository, NotificationRepository>();

            // Services
            builder.Services.AddScoped<IPatientService, PatientService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<ITreatmentTypeService, TreatmentTypeService>();
            builder.Services.AddScoped<IDocumentService, DocumentService>();
            builder.Services.AddScoped<IBookingService, BookingService>();
            builder.Services.AddScoped<INotificationService, NotificationService>();

            // Seeder
            builder.Services.AddScoped<DatabaseSeeder>();

            // Controllers, Swagger
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            builder.Services.AddCors();

            
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
            // Registrera din ExceptionMiddleware här (före andra middlewares)

            app.UseMiddleware<ExceptionMiddleware.ExceptionMiddleware>();
            
            app.UseCors(builder =>
            {
                builder
                    .AllowAnyOrigin() // eller .WithOrigins("https://localhost:44312") för mer kontroll
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });

            
            // Middleware
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
