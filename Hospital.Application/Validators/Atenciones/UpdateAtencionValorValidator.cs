using FluentValidation;
using Hospital.Application.DTOs.Atenciones;

namespace Hospital.Application.Validators.Atenciones;

public class UpdateAtencionValorValidator : AbstractValidator<UpdateAtencionValorDto>
{
    public UpdateAtencionValorValidator()
    {
        RuleFor(x => x.ValorTexto)
            .MaximumLength(2000);
    }
}
