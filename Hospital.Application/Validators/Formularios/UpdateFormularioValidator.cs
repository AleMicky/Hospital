using FluentValidation;
using Hospital.Application.DTOs.Formularios;

namespace Hospital.Application.Validators.Formularios;

public class UpdateFormularioValidator : AbstractValidator<UpdateFormularioDto>
{
    public UpdateFormularioValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.Descripcion)
            .MaximumLength(500);

        RuleFor(x => x.Version)
            .GreaterThan(0);
    }
}
