using FluentValidation;
using BookingSystem.Application.DTO;

namespace BookingSystem.Application.Validators;

public class CreateRoleDtoValidator : AbstractValidator<CreateRoleDto>
{
    public CreateRoleDtoValidator()
    {
        RuleFor(x => x.RoleName)
            .NotEmpty().WithMessage("Role name is required")
            .MaximumLength(10).WithMessage("Role name cannot exceed 10 characters");
    }
}