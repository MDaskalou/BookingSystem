using System.Collections.Generic;
using BookingSystem.Application.Common;
using BookingSystem.Application.DTO;
using MediatR;

namespace BookingSystem.Application.Queries.QueriesBooking.GetAllBookings;

public record GetAllBookingsQuery() : IRequest<OperationResult<List<BookingDto>>>;