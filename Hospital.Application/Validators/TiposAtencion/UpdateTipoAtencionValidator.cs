using FluentValidation;
using Hospital.Application.DTOs.TiposAtencion;

namespace Hospital.Application.Validators.TiposAtencion;

public class UpdateTipoAtencionValidator : AbstractValidator<UpdateTipoAtencionDto>
{
    public UpdateTipoAtencionValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Descripcion)
            .MaximumLength(250);

        RuleFor(x => x.Color)
            .MaximumLength(20);

        RuleFor(x => x.Icono)
            .MaximumLength(50);
    }
}
