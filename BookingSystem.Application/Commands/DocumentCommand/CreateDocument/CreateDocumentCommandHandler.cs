using System;
using System.Threading;
using System.Threading.Tasks;
using BookingSystem.Application.Common;
using BookingSystem.Application.DTO;
using BookingSystem.Domain.Entities;
using BookingSystem.Infrastructure;
using MediatR;

namespace BookingSystem.Application.Commands.DocumentCommand.CreateDocument;

public class CreateDocumentCommandHandler : IRequestHandler<CreateDocumentCommand, OperationResult<DocumentDto>>
{
    private readonly AppDbContext _context;

    public CreateDocumentCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<OperationResult<DocumentDto>> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = new Document
            {
                FileName = request.Dto.FileName,
                Verified = request.Dto.Verified,
                UploadedByUserId = request.Dto.UploadedByUserId
            };

            _context.Documents.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            
            var dto = new DocumentDto
            {
                DocumentId = entity.DocumentId,
                FileName = entity.FileName,
                Verified = entity.Verified,
                UploadedByUserId = entity.UploadedByUserId
            };
            return OperationResult<DocumentDto>.Ok(dto, "Dokument skapat.");

        }
        catch (Exception ex)
        {
            return OperationResult<DocumentDto>.Fail($"Ett fel uppstod: {ex.Message}");
        }
       
    }
}