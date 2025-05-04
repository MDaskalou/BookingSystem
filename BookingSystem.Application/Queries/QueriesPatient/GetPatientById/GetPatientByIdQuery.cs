using BookingSystem.Application.DTO;
using MediatR;

namespace BookingSystem.Application.Queries.QueriesPatient.GetPatientById;

public record GetPatientByIdQuery(int Id) : IRequest<PatientDto?>;