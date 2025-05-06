using BookingSystem.Application.Common;
using BookingSystem.Application.DTO;
using MediatR;

namespace BookingSystem.Application.Queries.QueriesBooking.GetBookingById;

public record GetBookingByIdQuery(int Id) : IRequest<OperationResult<BookingDto>?>;