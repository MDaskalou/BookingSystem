using System.Threading;
using System.Threading.Tasks;
using BookingSystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Application.Commands.PatientCommand.DeletePatient;

public class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommand, bool>
{
    private readonly AppDbContext _context;

    public DeletePatientCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
    {
        var patient = await _context.Patients
            .FirstOrDefaultAsync(p => p.PatientId == request.Id, cancellationToken);

        if (patient == null) return false;

        _context.Patients.Remove(patient);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}