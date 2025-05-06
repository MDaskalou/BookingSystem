using System.Threading;
using System.Threading.Tasks;
using BookingSystem.Application.Common;
using BookingSystem.Application.DTO;
using BookingSystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Application.Queries.QueriesDocument.GetDocumentById;

public class GetDocumentByIdQueryHandler : IRequestHandler<GetDocumentByIdQuery, OperationResult<DocumentDto>>
{
    private readonly AppDbContext _context;

    public GetDocumentByIdQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<OperationResult<DocumentDto>> Handle(GetDocumentByIdQuery request, CancellationToken cancellationToken)
    {
        var doc = await _context.Documents.FindAsync(request.Id);
        
        if (doc == null)
            return OperationResult<DocumentDto>.Fail("Document not found");
        
        var dto = new DocumentDto
        {
            DocumentId = doc.DocumentId,
            FileName = doc.FileName,
            Verified = doc.Verified,
            UploadedByUserId = doc.UploadedByUserId
        };
        
        return OperationResult<DocumentDto>.Ok(dto);
    }
}