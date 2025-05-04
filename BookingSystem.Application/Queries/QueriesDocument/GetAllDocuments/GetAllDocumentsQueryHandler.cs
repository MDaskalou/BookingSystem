using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookingSystem.Application.DTO;
using BookingSystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Application.Queries.QueriesDocument.GetAllDocuments;

public class GetAllDocumentsQueryHandler : IRequestHandler<GetAllDocumentsQuery, IEnumerable<DocumentDto>>
{
    private readonly AppDbContext _context;

    public GetAllDocumentsQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DocumentDto>> Handle(GetAllDocumentsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Documents
            .Select(d => new DocumentDto
            {
                DocumentId = d.DocumentId,
                FileName = d.FileName,
                Verified = d.Verified,
                UploadedByUserId = d.UploadedByUserId
            })
            .ToListAsync(cancellationToken);
    }
}