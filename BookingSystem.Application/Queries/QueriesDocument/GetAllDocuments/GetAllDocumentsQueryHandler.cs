using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using BookingSystem.Application.Common;
using BookingSystem.Application.DTO;
using BookingSystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Application.Queries.QueriesDocument.GetAllDocuments;

public class GetAllDocumentsQueryHandler : IRequestHandler<GetAllDocumentsQuery, OperationResult<List<DocumentDto>>>
{
    private readonly AppDbContext _context;

    public GetAllDocumentsQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<OperationResult<List<DocumentDto>>> Handle(GetAllDocumentsQuery request, CancellationToken cancellationToken)
    {
        var docs = await _context.Documents.ToListAsync(cancellationToken);

        var dto = docs.Select(d => new DocumentDto
        {
            DocumentId = d.DocumentId,
            FileName = d.FileName,
            Verified = d.Verified,
            UploadedByUserId = d.UploadedByUserId
        });
            return OperationResult<List<DocumentDto>>.Ok(dto.ToList());
    }
}