using FluentValidation;
using Hospital.Application.DTOs.Roles;

namespace Hospital.Application.Validators;

public class CreateRoleValidator : AbstractValidator<CreateRoleDto>
{
    public CreateRoleValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(256);
    }
}
