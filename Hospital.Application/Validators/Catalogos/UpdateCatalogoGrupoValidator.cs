using FluentValidation;
using Hospital.Application.DTOs.Catalogo;

namespace Hospital.Application.Validators.Catalogos;

public class UpdateCatalogoGrupoValidator
    : AbstractValidator<UpdateCatalogoGrupoDto>
{
    public UpdateCatalogoGrupoValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty()
            .WithMessage("El nombre es obligatorio.")
            .MaximumLength(100);

        RuleFor(x => x.Descripcion)
            .MaximumLength(250);
    }
}