using BookingSystem.Application.DTO;
using MediatR;

namespace BookingSystem.Application.Commands.TreatmentTypeCommands.CreateTreatmentType;

public record CreateTreatmentTypeCommand(CreateTreatmentTypeDto Dto) : IRequest<TreatmentTypeDto>;