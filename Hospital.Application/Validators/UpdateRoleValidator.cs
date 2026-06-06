using FluentValidation;
using Hospital.Application.DTOs.Roles;

namespace Hospital.Application.Validators;

public class UpdateRoleValidator : AbstractValidator<UpdateRoleDto>
{
    public UpdateRoleValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(256);
    }
}
