using BookingSystem.Application.DTO;
using MediatR;

namespace BookingSystem.Application.Queries.QueriesDocument.GetDocumentById;

public record GetDocumentByIdQuery(int Id) : IRequest<DocumentDto?>;