using System.Text;
using BookingSystem.Application.Authorization;
using BookingSystem.Application.Behaviors;
using BookingSystem.Application.Queries.GetAllUsers;
using BookingSystem.Application.Service.Implementation;
using BookingSystem.Application.Validators;
using BookingSystem.Infrastructure;
using BookingSystem.Infrastructure.DataBase;
using BookingSystem.Infrastructure.IRepository;
using BookingSystem.Infrastructure.Repositories;
using BookingSystem.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using BookingSystem.Domain.Entities;
using MediatR;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;


namespace API
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // --- ARC Configuration section ---
            // Set up DbContext
            var jwtKey = builder.Configuration["Jwt:SecretKey"];
            var jwtIssuer = builder.Configuration["Jwt:Issuer"];
            
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
            //builder.Services.AddScoped<IPatientService, PatientService>();
            //builder.Services.AddScoped<IUserService, UserService>();
            //builder.Services.AddScoped<IRoleService, RoleService>();
            //builder.Services.AddScoped<ITreatmentTypeService, TreatmentTypeService>();
            //builder.Services.AddScoped<IDocumentService, DocumentService>();
            //builder.Services.AddScoped<IBookingService, BookingService>();
            //builder.Services.AddScoped<INotificationService, NotificationService>();
           
            builder.Services.AddScoped<AuthService>();
            builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("OnlyAdmin", policy =>
                    policy.Requirements.Add(new RoleRequirement("Admin")));
            });
            builder.Services.AddSingleton<IAuthorizationHandler, RoleRequirementHandler>();

            
            
            //Mediatr
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(GetAllUsersQuery).Assembly);
            });
            
            
            //pipline
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionHandlingBehavior<,>));
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            
            //Validators
            builder.Services.AddValidatorsFromAssemblyContaining<CreateBookingDtoValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<CreatePatientDtoValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateNotificationDtoValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateRoleDtoValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateTreatmentTypeDtoValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateDocumentDtoValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<UpdateUserDtoValidator>();


            



            
            builder.Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtIssuer,
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!))
                    };
                });

            // Seeder
            builder.Services.AddScoped<DatabaseSeeder>();

            // Controllers, Swagger
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "BookingSystem API", Version = "v1" });

                // Lägg till JWT-beskrivning i Swagger
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Skriv in 'Bearer {ditt token}' här. Exempel: 'Bearer eyJhbGciOi...'"
                });

                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        []
                    }
                });
            });

            
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
            
            app.UseCors(corsPolicyBuilder =>
            {
                corsPolicyBuilder
                    .AllowAnyOrigin() // eller .WithOrigins("https://localhost:44312") för mer kontroll
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });

            
            // Middleware
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            
            

            app.Run();
        }
    }
}
