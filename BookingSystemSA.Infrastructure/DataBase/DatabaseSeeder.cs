using BookingSystemSA.Infrastructure.Fakers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Infrastructure.DataBase
{
    public class DatabaseSeeder
    {
        private readonly AppDbContext _context;

        public DatabaseSeeder(AppDbContext context)
        {
            _context = context;
        }

        public void SeedData()
        {
            

            // Skapa användare
            var userFaker = new UserFaker();
            var fakeUsers = userFaker.GenerateUser().Generate(10);  // Skapa 10 användare
            _context.Users.AddRange(fakeUsers);

            // Skapa dokument
            var documentFaker = new DocumentFaker();
            var fakeDocuments = documentFaker.GenerateDocument(fakeUsers).Generate(10);
            // var fakeDocuments = documentFaker.GenerateDocument().Generate(10);  // Skapa 10 dokument
            _context.Documents.AddRange(fakeDocuments);

            // Skapa behandlingstyper
            var treatmentTypeFaker = new TreatmentTypeFaker();
            var fakeTreatmentTypes = treatmentTypeFaker.GenerateTreatmentType().Generate(5);  // Skapa 5 behandlingstyper
            _context.TreatmentTypes.AddRange(fakeTreatmentTypes);

            // Skapa roller
            var roleFaker = new RoleFaker();
            var fakeRoles = roleFaker.GenerateRole().Generate(3);  // Skapa 3 roller (t.ex. Admin, User, Manager)
            _context.Roles.AddRange(fakeRoles);

            // Spara till databasen
            _context.SaveChanges();
        }

    }
}
