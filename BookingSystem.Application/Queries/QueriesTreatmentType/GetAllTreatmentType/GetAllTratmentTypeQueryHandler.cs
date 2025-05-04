using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookingSystem.Application.DTO;
using BookingSystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Application.Queries.QueriesTreatmentType.GetAllTreatmentType;

public class GetAllTreatmentTypesQueryHandler : IRequestHandler<GetAllTreatmentTypesQuery, IEnumerable<TreatmentTypeDto>>
{
    private readonly AppDbContext _context;

    public GetAllTreatmentTypesQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TreatmentTypeDto>> Handle(GetAllTreatmentTypesQuery request, CancellationToken cancellationToken)
    {
        return await _context.TreatmentTypes
            .Select(tt => new TreatmentTypeDto
            {
                TreatmentTypeId = tt.TreatmentTypeId,
                Name = tt.Name
            })
            .ToListAsync(cancellationToken);
    }
}