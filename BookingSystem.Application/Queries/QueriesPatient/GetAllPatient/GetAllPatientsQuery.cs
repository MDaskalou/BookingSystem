using System.Collections.Generic;
using BookingSystem.Application.DTO;
using MediatR;

namespace BookingSystem.Application.Queries.QueriesPatient.GetAllPatient;

public record GetAllPatientsQuery() : IRequest<IEnumerable<PatientDto>>;