using System.Threading;
using System.Threading.Tasks;
using BookingSystem.Application.DTO;
using BookingSystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Application.Queries.QueriesTreatmentType.GetAllTreatmentTypeById;

public class GetTreatmentTypeByIdQueryHandler : IRequestHandler<GetTreatmentTypeByIdQuery, TreatmentTypeDto?>
{
    private readonly AppDbContext _context;

    public GetTreatmentTypeByIdQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TreatmentTypeDto?> Handle(GetTreatmentTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.TreatmentTypes
            .FirstOrDefaultAsync(t => t.TreatmentTypeId == request.Id, cancellationToken);

        if (entity == null) return null;

        return new TreatmentTypeDto
        {
            TreatmentTypeId = entity.TreatmentTypeId,
            Name = entity.Name
        };
    }
}