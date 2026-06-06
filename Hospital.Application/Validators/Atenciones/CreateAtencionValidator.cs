using FluentValidation;
using Hospital.Application.DTOs.Atenciones;

namespace Hospital.Application.Validators.Atenciones;

public class CreateAtencionValidator : AbstractValidator<CreateAtencionDto>
{
    public CreateAtencionValidator()
    {
        RuleFor(x => x.PacienteId)
            .GreaterThan(0);

        RuleFor(x => x.TipoAtencionId)
            .GreaterThan(0);

        RuleFor(x => x.FormularioId)
            .GreaterThan(0);

        RuleFor(x => x.Estado)
            .NotEmpty()
            .MaximumLength(30);

        RuleFor(x => x.MotivoConsulta)
            .MaximumLength(500);
    }
}
