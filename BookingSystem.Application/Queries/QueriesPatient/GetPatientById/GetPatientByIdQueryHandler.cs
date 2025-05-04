using System.Threading;
using System.Threading.Tasks;
using BookingSystem.Application.DTO;
using BookingSystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Application.Queries.QueriesPatient.GetPatientById;

public class GetPatientByIdQueryHandler : IRequestHandler<GetPatientByIdQuery, PatientDto?>
{
    private readonly AppDbContext _context;

    public GetPatientByIdQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PatientDto?> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
    {
        var patient = await _context.Patients
            .FirstOrDefaultAsync(p => p.PatientId == request.Id, cancellationToken);

        if (patient == null) return null;

        return new PatientDto
        {
            PatientId = patient.PatientId,
            FullName = patient.FullName,
            SocialSecurityNumber = patient.SocialSecurityNumber
        };
    }
}