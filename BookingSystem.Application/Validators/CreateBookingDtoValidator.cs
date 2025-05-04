using System;
using FluentValidation;
using BookingSystem.Application.DTO;

namespace BookingSystem.Application.Validators;

public class CreateBookingDtoValidator : AbstractValidator<CreateBookingDto>
{
    public CreateBookingDtoValidator()
    {
        RuleFor(x => x.Date)
            .GreaterThan(DateTime.MinValue).WithMessage("Date is required");

        RuleFor(x => x.PatientId)
            .GreaterThan(0).WithMessage("PatientId must be valid");

        RuleFor(x => x.TreatmentTypeId)
            .GreaterThan(0).WithMessage("TreatmentTypeId must be valid");

        RuleFor(x => x.CreatedById)
            .GreaterThan(0).WithMessage("CreatedById must be valid");

        RuleFor(x => x.Priority)
            .IsInEnum().WithMessage("Invalid priority");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Invalid status");
    }
}