using BookingSystem.Application.DTO;
using FluentValidation;

namespace BookingSystem.Application.Validators;

public partial class UpdateUserDtoValidator :  AbstractValidator<UpdateUserDto>
{
    public UpdateUserDtoValidator()
    {
        RuleFor(x =>x.Fullname).NotEmpty().WithMessage("Fullname is required.");
        
        RuleFor(x =>x.Email).NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email is invalid.");;
        
        RuleFor(x => x.Password)
            .MinimumLength(6).When(x => !string.IsNullOrWhiteSpace(x.Password))
            .WithMessage("Password must be at least 6 characters");    }
}