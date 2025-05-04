using BookingSystem.Application.DTO;
using MediatR;

namespace BookingSystem.Application.Commands.TreatmentTypeCommands.UpdateTreatmentType;

public record UpdateTreatmentTypeCommand(int Id, CreateTreatmentTypeDto Dto) : IRequest<bool>;