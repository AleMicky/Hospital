using FluentValidation;
using Hospital.Application.DTOs.Catalogos;

namespace Hospital.Application.Validators.Catalogos;

public class CreatePrestacionValidator : AbstractValidator<CreatePrestacionDto>
{
    public CreatePrestacionValidator()
    {
        RuleFor(x => x.ServicioId)
            .GreaterThan(0);

        RuleFor(x => x.Codigo)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Nombre)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.Descripcion)
            .MaximumLength(300);

        RuleFor(x => x.Precio)
            .GreaterThanOrEqualTo(0);
    }
}

public class UpdatePrestacionValidator : AbstractValidator<UpdatePrestacionDto>
{
    public UpdatePrestacionValidator()
    {
        RuleFor(x => x.ServicioId)
            .GreaterThan(0);

        RuleFor(x => x.Codigo)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Nombre)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.Descripcion)
            .MaximumLength(300);

        RuleFor(x => x.Precio)
            .GreaterThanOrEqualTo(0);
    }
}
