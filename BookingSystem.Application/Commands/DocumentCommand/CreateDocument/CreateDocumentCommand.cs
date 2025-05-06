using BookingSystem.Application.Common;
using BookingSystem.Application.DTO;
using MediatR;

namespace BookingSystem.Application.Commands.DocumentCommand.CreateDocument;

public record CreateDocumentCommand(CreateDocumentDto Dto) : IRequest <OperationResult<DocumentDto>>;