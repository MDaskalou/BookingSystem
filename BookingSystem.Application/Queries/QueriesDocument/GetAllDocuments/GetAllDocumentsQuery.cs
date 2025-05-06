using System.Collections.Generic;
using BookingSystem.Application.Common;
using BookingSystem.Application.DTO;
using MediatR;

namespace BookingSystem.Application.Queries.QueriesDocument.GetAllDocuments;

public record GetAllDocumentsQuery() : IRequest<OperationResult<List<DocumentDto>>>;