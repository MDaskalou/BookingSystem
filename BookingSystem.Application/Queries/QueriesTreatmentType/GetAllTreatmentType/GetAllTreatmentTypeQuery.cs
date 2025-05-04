using System.Collections.Generic;
using BookingSystem.Application.DTO;
using MediatR;

namespace BookingSystem.Application.Queries.QueriesTreatmentType.GetAllTreatmentType;

public class GetAllTreatmentTypeQuery;

public record GetAllTreatmentTypesQuery() : IRequest<IEnumerable<TreatmentTypeDto>>;
