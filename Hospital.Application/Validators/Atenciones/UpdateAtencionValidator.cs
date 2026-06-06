using FluentValidation;
using Hospital.Application.DTOs.Atenciones;

namespace Hospital.Application.Validators.Atenciones;

public class UpdateAtencionValidator : AbstractValidator<UpdateAtencionDto>
{
    public UpdateAtencionValidator()
    {
        RuleFor(x => x.Estado)
            .NotEmpty()
            .MaximumLength(30);

        RuleFor(x => x.MotivoConsulta)
            .MaximumLength(500);
    }
}
