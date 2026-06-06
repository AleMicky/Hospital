using Hospital.Application.DTOs.Pacientes;
using Hospital.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Hospital.Infrastructure.Mappings;

[Mapper(
    RequiredMappingStrategy = RequiredMappingStrategy.None
)]
public partial class PacienteMapper
{
    [MapperIgnoreTarget(nameof(Paciente.Id))]
    [MapperIgnoreTarget(nameof(Paciente.CreatedAt))]
    [MapperIgnoreTarget(nameof(Paciente.CreatedBy))]
    [MapperIgnoreTarget(nameof(Paciente.UpdatedAt))]
    [MapperIgnoreTarget(nameof(Paciente.UpdatedBy))]
    [MapperIgnoreTarget(nameof(Paciente.Activo))]
    [MapperIgnoreTarget(nameof(Paciente.Atenciones))]
    [MapperIgnoreTarget(nameof(Paciente.TipoDocumento))]
    [MapperIgnoreTarget(nameof(Paciente.ExtensionDocumento))]
    [MapperIgnoreTarget(nameof(Paciente.Sexo))]
    [MapperIgnoreTarget(nameof(Paciente.EstadoCivil))]
    public partial Paciente ToEntity(CreatePacienteDto dto);

    [MapperIgnoreTarget(nameof(Paciente.Id))]
    [MapperIgnoreTarget(nameof(Paciente.CodigoPaciente))]
    [MapperIgnoreTarget(nameof(Paciente.CreatedAt))]
    [MapperIgnoreTarget(nameof(Paciente.CreatedBy))]
    [MapperIgnoreTarget(nameof(Paciente.UpdatedAt))]
    [MapperIgnoreTarget(nameof(Paciente.UpdatedBy))]
    [MapperIgnoreTarget(nameof(Paciente.Activo))]
    [MapperIgnoreTarget(nameof(Paciente.Atenciones))]
    [MapperIgnoreTarget(nameof(Paciente.TipoDocumento))]
    [MapperIgnoreTarget(nameof(Paciente.ExtensionDocumento))]
    [MapperIgnoreTarget(nameof(Paciente.Sexo))]
    [MapperIgnoreTarget(nameof(Paciente.EstadoCivil))]
    public partial void UpdateEntity(UpdatePacienteDto dto, Paciente entity);

    [MapperIgnoreTarget(nameof(PacienteResponseDto.TipoDocumento))]
    [MapperIgnoreTarget(nameof(PacienteResponseDto.ExtensionDocumento))]
    [MapperIgnoreTarget(nameof(PacienteResponseDto.Sexo))]
    [MapperIgnoreTarget(nameof(PacienteResponseDto.EstadoCivil))]
    [MapperIgnoreTarget(nameof(PacienteResponseDto.Edad))]
    public partial PacienteResponseDto ToDto(Paciente entity);
}