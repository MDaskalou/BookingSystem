using System.Threading;
using System.Threading.Tasks;
using BookingSystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Application.Commands.DocumentCommand.DeleteDocument;

public class DeleteDocumentCommandHandler : IRequestHandler<DeleteDocumentCommand, bool>
{
    private readonly AppDbContext _context;

    public DeleteDocumentCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
    {
        var doc = await _context.Documents.FirstOrDefaultAsync(d => d.DocumentId == request.Id, cancellationToken);
        if (doc == null) return false;

        _context.Documents.Remove(doc);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}