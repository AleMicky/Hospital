using FluentValidation;
using Hospital.Application.DTOs.Formularios;

namespace Hospital.Application.Validators.Formularios;

public class CreateFormularioCampoValidator : AbstractValidator<CreateFormularioCampoDto>
{
    public CreateFormularioCampoValidator()
    {
        RuleFor(x => x.FormularioId)
            .GreaterThan(0);

        RuleFor(x => x.NombreCampo)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Etiqueta)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.Seccion)
            .MaximumLength(100);

        RuleFor(x => x.Placeholder)
            .MaximumLength(200);

        RuleFor(x => x.ValorDefault)
            .MaximumLength(500);

        RuleFor(x => x.OpcionesJson)
            .MaximumLength(2000);
    }
}
