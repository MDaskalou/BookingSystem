using BookingSystem.Application.DTO;
using MediatR;

namespace BookingSystem.Application.Commands.PatientCommand.UpdatePatient;

public record UpdatePatientCommand(int Id, CreatePatientDto Dto) : IRequest<bool>;