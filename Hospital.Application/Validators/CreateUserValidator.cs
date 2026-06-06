using FluentValidation;
using Hospital.Application.DTOs.Users;

namespace Hospital.Application.Validators;

public class CreateUserValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .MaximumLength(256);

        RuleFor(x => x.NombreCompleto)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6);

        RuleFor(x => x.Rol)
            .NotEmpty()
            .MaximumLength(256);
    }
}
