using FluentValidation;
using Hospital.Application.DTOs.Catalogos;

namespace Hospital.Application.Validators.Catalogos;

public class CreateDepartamentoValidator : AbstractValidator<CreateDepartamentoDto>
{
    public CreateDepartamentoValidator()
    {
        RuleFor(x => x.AreaId)
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

public class UpdateDepartamentoValidator : AbstractValidator<UpdateDepartamentoDto>
{
    public UpdateDepartamentoValidator()
    {
        RuleFor(x => x.AreaId)
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
