using System.Threading;
using System.Threading.Tasks;
using BookingSystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Application.Commands.PatientCommand.UpdatePatient;

public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand, bool>
{
    private readonly AppDbContext _context;

    public UpdatePatientCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
    {
        var patient = await _context.Patients
            .FirstOrDefaultAsync(p => p.PatientId == request.Id, cancellationToken);

        if (patient == null) return false;

        patient.FullName = request.Dto.FullName;
        patient.SocialSecurityNumber = request.Dto.SocialSecurityNumber;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}