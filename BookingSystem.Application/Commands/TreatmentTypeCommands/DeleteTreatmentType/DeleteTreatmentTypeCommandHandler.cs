using System.Threading;
using System.Threading.Tasks;
using BookingSystem.Application.Commands.TreatmentTypeCommands.DeleteTreatmentType;
using BookingSystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Application.TreatmentTypes.Commands.DeleteTreatmentType;

public class DeleteTreatmentTypeCommandHandler : IRequestHandler<DeleteTreatmentTypeCommand, bool>
{
    private readonly AppDbContext _context;

    public DeleteTreatmentTypeCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteTreatmentTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TreatmentTypes
            .FirstOrDefaultAsync(t => t.TreatmentTypeId == request.Id, cancellationToken);

        if (entity == null) return false;

        _context.TreatmentTypes.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}