using FluentValidation;
using Hospital.Application.DTOs.Users;

namespace Hospital.Application.Validators;

public class UpdateUserValidator : AbstractValidator<UpdateUserDto>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.NombreCompleto)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Rol)
            .NotEmpty()
            .MaximumLength(256);

        RuleFor(x => x.Password)
            .MinimumLength(6)
            .When(x => !string.IsNullOrWhiteSpace(x.Password));
    }
}
