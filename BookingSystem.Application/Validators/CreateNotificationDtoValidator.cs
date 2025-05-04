using FluentValidation;
using BookingSystem.Application.DTO;

namespace BookingSystem.Application.Validators;

public class CreateNotificationDtoValidator : AbstractValidator<CreateNotificationDto>
{
    public CreateNotificationDtoValidator()
    {
        RuleFor(x => x.Message)
            .NotEmpty().WithMessage("Message is required")
            .MaximumLength(500).WithMessage("Message cannot exceed 500 characters");

        RuleFor(x => x.RecipientId)
            .GreaterThan(0).WithMessage("RecipientId must be a valid user ID");
    }
}