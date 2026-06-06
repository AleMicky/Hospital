using FluentValidation;
using Hospital.Application.DTOs.Pacientes;

namespace Hospital.Application.Validators.Pacientes;

public class UpdatePacienteValidator : AbstractValidator<UpdatePacienteDto>
{
    public UpdatePacienteValidator()
    {
        RuleFor(x => x.Nombres)
            .NotEmpty()
            .WithMessage("Los nombres son obligatorios.")
            .MaximumLength(150);

        RuleFor(x => x.ApellidoPaterno)
            .NotEmpty()
            .WithMessage("El apellido paterno es obligatorio.")
            .MaximumLength(100);

        RuleFor(x => x.ApellidoMaterno)
            .MaximumLength(100);

        RuleFor(x => x.TipoDocumentoId)
            .GreaterThan(0)
            .WithMessage("Debe seleccionar un tipo de documento.");

        RuleFor(x => x.NumeroDocumento)
            .NotEmpty()
            .WithMessage("El número de documento es obligatorio.")
            .MaximumLength(20);

        RuleFor(x => x.ComplementoDocumento)
            .MaximumLength(10);

        RuleFor(x => x.ExtensionDocumentoId)
            .GreaterThan(0)
            .When(x => x.ExtensionDocumentoId.HasValue)
            .WithMessage("La extensión del documento no es válida.");

        RuleFor(x => x.FechaNacimiento)
            .LessThan(DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("La fecha de nacimiento no es válida.");

        RuleFor(x => x.SexoId)
            .GreaterThan(0)
            .WithMessage("Debe seleccionar un sexo.");

        RuleFor(x => x.EstadoCivilId)
            .GreaterThan(0)
            .WithMessage("Debe seleccionar un estado civil.");

        RuleFor(x => x.Telefono)
            .MaximumLength(30);

        RuleFor(x => x.Direccion)
            .MaximumLength(250);

        RuleFor(x => x.OcupacionProfesion)
            .MaximumLength(150);
    }
}