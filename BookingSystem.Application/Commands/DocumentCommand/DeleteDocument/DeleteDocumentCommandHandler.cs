using System.Threading;
using System.Threading.Tasks;
using BookingSystem.Application.Common;
using BookingSystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Application.Commands.DocumentCommand.DeleteDocument;

public class DeleteDocumentCommandHandler : IRequestHandler<DeleteDocumentCommand, OperationResult<bool>>
{
    private readonly AppDbContext _context;

    public DeleteDocumentCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<OperationResult<bool>> Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
    {
        var doc = await _context.Documents.FindAsync(request.Id);
        
        if(doc == null)
            return OperationResult<bool>.Fail("Document could not found ") ;
        _context.Documents.Remove(doc);
        await _context.SaveChangesAsync(cancellationToken);
        
        return OperationResult<bool>.Ok(true, "Documentet has been erased.");
    }
}