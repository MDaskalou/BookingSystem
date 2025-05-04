using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookingSystem.Application.DTO;
using BookingSystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Application.Queries.QueriesPatient.GetAllPatient;

public class GetAllPatientsQueryHandler : IRequestHandler<GetAllPatientsQuery, IEnumerable<PatientDto>>
{
    private readonly AppDbContext _context;

    public GetAllPatientsQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PatientDto>> Handle(GetAllPatientsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Patients
            .Select(p => new PatientDto
            {
                PatientId = p.PatientId,
                FullName = p.FullName,
                SocialSecurityNumber = p.SocialSecurityNumber
            })
            .ToListAsync(cancellationToken);
    }
}