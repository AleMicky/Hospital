using FluentValidation;
using Hospital.Application.DTOs.Atenciones;

namespace Hospital.Application.Validators.Atenciones;

public class CreateAtencionValorValidator : AbstractValidator<CreateAtencionValorDto>
{
    public CreateAtencionValorValidator()
    {
        RuleFor(x => x.AtencionId)
            .GreaterThan(0);

        RuleFor(x => x.FormularioCampoId)
            .GreaterThan(0);

        RuleFor(x => x.ValorTexto)
            .MaximumLength(2000);
    }
}
