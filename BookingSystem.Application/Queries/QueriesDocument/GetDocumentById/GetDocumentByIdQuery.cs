using BookingSystem.Application.Common;
using BookingSystem.Application.DTO;
using MediatR;

namespace BookingSystem.Application.Queries.QueriesDocument.GetDocumentById;

public record GetDocumentByIdQuery(int Id) : IRequest<OperationResult<DocumentDto>?>;