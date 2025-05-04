using FluentValidation;
using BookingSystem.Application.DTO;

namespace BookingSystem.Application.Validators
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(x => x.Fullname)
                .NotEmpty()
                .WithMessage("Fullname is required.")
                .Length(2, 100)
                .WithMessage("Fullname must be between 2 and 100 characters.");


            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Invalid email format.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required.")
                .MinimumLength(6)
                .WithMessage("Password must be at least 6 characters long.");

            RuleFor(x => x.RoleId)
                .NotEmpty()
                .WithMessage("RoleId is required.")
                .GreaterThan(0)
                .WithMessage("RoleId must be greater than 0.");
        }
    }
    

    
}
