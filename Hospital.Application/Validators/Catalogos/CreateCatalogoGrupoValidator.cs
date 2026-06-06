using FluentValidation;
using Hospital.Application.DTOs.Catalogo;

namespace Hospital.Application.Validators.Catalogos;

public class CreateCatalogoGrupoValidator
    : AbstractValidator<CreateCatalogoGrupoDto>
{
    public CreateCatalogoGrupoValidator()
    {
        RuleFor(x => x.Codigo)
            .NotEmpty()
            .WithMessage("El código es obligatorio.")
            .MaximumLength(50);

        RuleFor(x => x.Nombre)
            .NotEmpty()
            .WithMessage("El nombre es obligatorio.")
            .MaximumLength(100);

        RuleFor(x => x.Descripcion)
            .MaximumLength(250);
    }
}