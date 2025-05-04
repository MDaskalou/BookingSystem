using MediatR;

namespace BookingSystem.Application.Commands.TreatmentTypeCommands.DeleteTreatmentType;

public record DeleteTreatmentTypeCommand(int Id) : IRequest<bool>;