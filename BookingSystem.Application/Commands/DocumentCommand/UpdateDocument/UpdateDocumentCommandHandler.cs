using System.Threading;
using System.Threading.Tasks;
using BookingSystem.Application.Common;
using BookingSystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Application.Commands.DocumentCommand.UpdateDocument;

public class UpdateDocumentCommandHandler : IRequestHandler<UpdateDocumentCommand, OperationResult<bool>>
{
    private readonly AppDbContext _context;

    public UpdateDocumentCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<OperationResult<bool>> Handle(UpdateDocumentCommand request, CancellationToken cancellationToken)
    {
        var doc = await _context.Documents.FirstOrDefaultAsync(d => d.DocumentId == request.Id, cancellationToken);
        
        if (doc == null) 
            return OperationResult<bool>.Fail("Document could not found ") ;

        doc.FileName = request.Dto.FileName;
        doc.Verified = request.Dto.Verified;
        doc.UploadedByUserId = request.Dto.UploadedByUserId;

        await _context.SaveChangesAsync(cancellationToken);
        return OperationResult<bool>.Ok(true, "Dokumentet har uppdaterats.");
    }
}