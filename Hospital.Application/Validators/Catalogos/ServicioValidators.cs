using FluentValidation;
using Hospital.Application.DTOs.Catalogos;

namespace Hospital.Application.Validators.Catalogos;

public class CreateServicioValidator : AbstractValidator<CreateServicioDto>
{
    public CreateServicioValidator()
    {
        RuleFor(x => x.DepartamentoId)
            .GreaterThan(0);

        RuleFor(x => x.Codigo)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Nombre)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.Descripcion)
            .MaximumLength(300);
    }
}

public class UpdateServicioValidator : AbstractValidator<UpdateServicioDto>
{
    public UpdateServicioValidator()
    {
        RuleFor(x => x.DepartamentoId)
            .GreaterThan(0);

        RuleFor(x => x.Codigo)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Nombre)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.Descripcion)
            .MaximumLength(300);
    }
}
