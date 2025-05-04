using FluentValidation;
using BookingSystem.Application.DTO;

namespace BookingSystem.Application.Validators;

public class CreateTreatmentTypeDtoValidator : AbstractValidator<CreateTreatmentTypeDto>
{
    public CreateTreatmentTypeDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Treatment type name is required")
            .MaximumLength(100).WithMessage("Treatment type name cannot exceed 100 characters");
    }
}