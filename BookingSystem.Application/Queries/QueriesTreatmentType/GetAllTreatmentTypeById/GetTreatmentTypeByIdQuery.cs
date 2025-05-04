using BookingSystem.Application.DTO;
using MediatR;

namespace BookingSystem.Application.Queries.QueriesTreatmentType.GetAllTreatmentTypeById;


public record GetTreatmentTypeByIdQuery(int Id) : IRequest<TreatmentTypeDto?>;
