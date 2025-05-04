using System.Threading;
using System.Threading.Tasks;
using BookingSystem.Application.DTO;
using BookingSystem.Domain.Entities;
using BookingSystem.Infrastructure;
using MediatR;

namespace BookingSystem.Application.Commands.TreatmentTypeCommands.CreateTreatmentType;

public class CreateTreatmentTypeCommandHandler : IRequestHandler<CreateTreatmentTypeCommand, TreatmentTypeDto>
{
    private readonly AppDbContext _context;

    public CreateTreatmentTypeCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TreatmentTypeDto> Handle(CreateTreatmentTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = new TreatmentType
        {
            Name = request.Dto.Name
        };

        _context.TreatmentTypes.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return new TreatmentTypeDto
        {
            TreatmentTypeId = entity.TreatmentTypeId,
            Name = entity.Name
        };
    }
}