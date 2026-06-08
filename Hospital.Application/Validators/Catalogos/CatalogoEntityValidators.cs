using FluentValidation;
using Hospital.Application.DTOs.Catalogos;

namespace Hospital.Application.Validators.Catalogos;

public class CreateAreaValidator : AbstractValidator<CreateAreaDto>
{
    public CreateAreaValidator()
    {
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

public class UpdateAreaValidator : AbstractValidator<UpdateAreaDto>
{
    public UpdateAreaValidator()
    {
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

public class CreateEspecialidadValidator : AbstractValidator<CreateEspecialidadDto>
{
    public CreateEspecialidadValidator()
    {
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

public class UpdateEspecialidadValidator : AbstractValidator<UpdateEspecialidadDto>
{
    public UpdateEspecialidadValidator()
    {
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

public class CreateProfesionValidator : AbstractValidator<CreateProfesionDto>
{
    public CreateProfesionValidator()
    {
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

public class UpdateProfesionValidator : AbstractValidator<UpdateProfesionDto>
{
    public UpdateProfesionValidator()
    {
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

public class CreateCargoValidator : AbstractValidator<CreateCargoDto>
{
    public CreateCargoValidator()
    {
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

public class UpdateCargoValidator : AbstractValidator<UpdateCargoDto>
{
    public UpdateCargoValidator()
    {
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

public class CreateTipoAtencionCatalogoValidator : AbstractValidator<CreateTipoAtencionCatalogoDto>
{
    public CreateTipoAtencionCatalogoValidator()
    {
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

public class UpdateTipoAtencionCatalogoValidator : AbstractValidator<UpdateTipoAtencionCatalogoDto>
{
    public UpdateTipoAtencionCatalogoValidator()
    {
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
