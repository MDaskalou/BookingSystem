using BookingSystem.Application.Common;
using MediatR;

namespace BookingSystem.Application.Commands.DocumentCommand.DeleteDocument;

public record DeleteDocumentCommand(int Id) : IRequest<OperationResult<bool>>;