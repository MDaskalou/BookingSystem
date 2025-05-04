using FluentValidation;
using BookingSystem.Application.DTO;

namespace BookingSystem.Application.Validators;

public class CreateDocumentDtoValidator : AbstractValidator<CreateDocumentDto>
{
    public CreateDocumentDtoValidator()
    {
        RuleFor(x => x.FileName)
            .NotEmpty().WithMessage("File name is required")
            .MaximumLength(200).WithMessage("File name cannot exceed 200 characters");

        RuleFor(x => x.UploadedByUserId)
            .GreaterThan(0).WithMessage("UploadedByUserId must be a valid user ID");
    }
}