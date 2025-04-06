using BookingSystem.Infrastructure.Fakers;


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
            // Kontrollera om roller redan finns innan du lägger till dem
            if (!_context.Roles.Any())
            {
                var roleFaker = new RoleFaker();
                var fakeRoles = roleFaker.GenerateRole().Generate(6);
                _context.Roles.AddRange(fakeRoles);
            }
            _context.SaveChanges();

            // Kontrollera om användare redan finns innan du lägger till dem
            if (!_context.Users.Any())
            {
                var userFaker = new UserFaker();
                var fakeUsers = userFaker.GenerateUser().Generate(10);
                _context.Users.AddRange(fakeUsers);
            }
            _context.SaveChanges();

            // Liknande kontroller för andra entiteter...
            if (!_context.Documents.Any())
            {
                var documentFaker = new DocumentFaker();
                var fakeUsers = _context.Users.ToList(); // Använd befintliga användare
                var fakeDocuments = documentFaker.GenerateDocument(fakeUsers).Generate(10);
                _context.Documents.AddRange(fakeDocuments);
            }
            _context.SaveChanges();

            if (!_context.Patients.Any())
            {
                var patientFaker = new PatientFaker();
                var fakePatients = patientFaker.GeneratePatient().Generate(10);
                _context.Patients.AddRange(fakePatients);
            }
            _context.SaveChanges();

            if (!_context.TreatmentTypes.Any())
            {
                var treatmentTypeFaker = new TreatmentTypeFaker();
                var fakeTreatmentTypes = treatmentTypeFaker.GenerateTreatmentType().Generate(5);
                _context.TreatmentTypes.AddRange(fakeTreatmentTypes);
            }

            // Spara till databasen
            _context.SaveChanges();

            if (!_context.Bookings.Any())
            {
                var bookingFaker = new BookingFaker();
                var fakeBookings = bookingFaker
                    .GenerateBooking(
                        _context.Patients.ToList(),
                        _context.TreatmentTypes.ToList(),
                        _context.Users.ToList())
                    .Generate(10);
                _context.Bookings.AddRange(fakeBookings);
            }
            _context.SaveChanges();


            if (!_context.Notifications.Any())
            {
                var notificationFaker = new NotificationFaker();
                var fakeNotifications = notificationFaker
                    .GenerateNotification(_context.Users.ToList())
                    .Generate(10);
                _context.Notifications.AddRange(fakeNotifications);
            }
            _context.SaveChanges();


        }

    }
}
