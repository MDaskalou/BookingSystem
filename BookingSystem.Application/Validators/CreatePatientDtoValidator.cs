using FluentValidation;
using BookingSystem.Application.DTO;

namespace BookingSystem.Application.Validators;

public class CreatePatientDtoValidator : AbstractValidator<CreatePatientDto>
{
    public CreatePatientDtoValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Full name is required");

        RuleFor(x => x.SocialSecurityNumber)
            .NotEmpty().WithMessage("Social security number is required")
            .Matches(@"^\d{6}-\d{4}$")
            .WithMessage("SSN must be in format YYMMDD-XXXX");
    }
}