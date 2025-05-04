using System.Threading;
using System.Threading.Tasks;
using BookingSystem.Application.DTO;
using BookingSystem.Domain.Entities;
using BookingSystem.Infrastructure;
using MediatR;

namespace BookingSystem.Application.Commands.DocumentCommand.CreateDocument;

public class CreateDocumentCommandHandler : IRequestHandler<CreateDocumentCommand, DocumentDto>
{
    private readonly AppDbContext _context;

    public CreateDocumentCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<DocumentDto> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
    {
        var entity = new Document
        {
            FileName = request.Dto.FileName,
            Verified = request.Dto.Verified,
            UploadedByUserId = request.Dto.UploadedByUserId
        };

        _context.Documents.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return new DocumentDto
        {
            DocumentId = entity.DocumentId,
            FileName = entity.FileName,
            Verified = entity.Verified,
            UploadedByUserId = entity.UploadedByUserId
        };
    }
}