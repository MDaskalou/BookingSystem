using System.Threading;
using System.Threading.Tasks;
using BookingSystem.Application.DTO;
using BookingSystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Application.Queries.QueriesDocument.GetDocumentById;

public class GetDocumentByIdQueryHandler : IRequestHandler<GetDocumentByIdQuery, DocumentDto?>
{
    private readonly AppDbContext _context;

    public GetDocumentByIdQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<DocumentDto?> Handle(GetDocumentByIdQuery request, CancellationToken cancellationToken)
    {
        var d = await _context.Documents.FirstOrDefaultAsync(x => x.DocumentId == request.Id, cancellationToken);
        if (d == null) return null;

        return new DocumentDto
        {
            DocumentId = d.DocumentId,
            FileName = d.FileName,
            Verified = d.Verified,
            UploadedByUserId = d.UploadedByUserId
        };
    }
}