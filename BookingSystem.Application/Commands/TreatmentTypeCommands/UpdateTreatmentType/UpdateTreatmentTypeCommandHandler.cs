using System.Threading;
using System.Threading.Tasks;
using BookingSystem.Application.Commands.TreatmentTypeCommands.UpdateTreatmentType;
using BookingSystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Application.TreatmentTypes.Commands.UpdateTreatmentType;

public class UpdateTreatmentTypeCommandHandler : IRequestHandler<UpdateTreatmentTypeCommand, bool>
{
    private readonly AppDbContext _context;

    public UpdateTreatmentTypeCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateTreatmentTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TreatmentTypes
            .FirstOrDefaultAsync(t => t.TreatmentTypeId == request.Id, cancellationToken);

        if (entity == null) return false;

        entity.Name = request.Dto.Name;
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}