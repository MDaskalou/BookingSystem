using System.Threading;
using System.Threading.Tasks;
using BookingSystem.Application.DTO;
using BookingSystem.Domain.Entities;
using BookingSystem.Infrastructure;
using MediatR;

namespace BookingSystem.Application.Commands.PatientCommand.CreatePatient;

public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, PatientDto>
{
    private readonly AppDbContext _context;

    public CreatePatientCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PatientDto> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
    {
        var patient = new Patient
        {
            FullName = request.Dto.FullName,
            SocialSecurityNumber = request.Dto.SocialSecurityNumber
        };

        _context.Patients.Add(patient);
        await _context.SaveChangesAsync(cancellationToken);

        return new PatientDto
        {
            PatientId = patient.PatientId,
            FullName = patient.FullName,
            SocialSecurityNumber = patient.SocialSecurityNumber
        };
    }
}