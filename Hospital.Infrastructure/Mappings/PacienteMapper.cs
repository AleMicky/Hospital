using Hospital.Application.DTOs.Pacientes;
using Hospital.Domain.Entities.Personas;
using Riok.Mapperly.Abstractions;

namespace Hospital.Infrastructure.Mappings;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None)]
public partial class PacienteMapper
{
    public Paciente ToEntity(CreatePacienteDto dto) =>
        new()
        {
            CodigoPaciente = dto.CodigoPaciente,
            OcupacionProfesion = dto.OcupacionProfesion,
            Persona = ToPersona(dto)
        };

    public void UpdateEntity(UpdatePacienteDto dto, Paciente entity)
    {
        entity.OcupacionProfesion = dto.OcupacionProfesion;
        UpdatePersona(dto, entity.Persona);
    }

    [MapperIgnoreTarget(nameof(Persona.Id))]
    [MapperIgnoreTarget(nameof(Persona.CreatedAt))]
    [MapperIgnoreTarget(nameof(Persona.CreatedBy))]
    [MapperIgnoreTarget(nameof(Persona.UpdatedAt))]
    [MapperIgnoreTarget(nameof(Persona.UpdatedBy))]
    [MapperIgnoreTarget(nameof(Persona.Activo))]
    [MapperIgnoreTarget(nameof(Persona.TipoDocumento))]
    [MapperIgnoreTarget(nameof(Persona.ExtensionDocumento))]
    [MapperIgnoreTarget(nameof(Persona.Sexo))]
    [MapperIgnoreTarget(nameof(Persona.EstadoCivil))]
    private partial Persona ToPersona(CreatePacienteDto dto);

    [MapperIgnoreTarget(nameof(Persona.Id))]
    [MapperIgnoreTarget(nameof(Persona.CreatedAt))]
    [MapperIgnoreTarget(nameof(Persona.CreatedBy))]
    [MapperIgnoreTarget(nameof(Persona.UpdatedAt))]
    [MapperIgnoreTarget(nameof(Persona.UpdatedBy))]
    [MapperIgnoreTarget(nameof(Persona.Activo))]
    [MapperIgnoreTarget(nameof(Persona.TipoDocumento))]
    [MapperIgnoreTarget(nameof(Persona.ExtensionDocumento))]
    [MapperIgnoreTarget(nameof(Persona.Sexo))]
    [MapperIgnoreTarget(nameof(Persona.EstadoCivil))]
    private partial void UpdatePersona(UpdatePacienteDto dto, Persona persona);

    [MapProperty(nameof(Paciente.Persona.Nombres), nameof(PacienteResponseDto.Nombres))]
    [MapProperty(nameof(Paciente.Persona.ApellidoPaterno), nameof(PacienteResponseDto.ApellidoPaterno))]
    [MapProperty(nameof(Paciente.Persona.ApellidoMaterno), nameof(PacienteResponseDto.ApellidoMaterno))]
    [MapProperty(nameof(Paciente.Persona.TipoDocumentoId), nameof(PacienteResponseDto.TipoDocumentoId))]
    [MapProperty(nameof(Paciente.Persona.NumeroDocumento), nameof(PacienteResponseDto.NumeroDocumento))]
    [MapProperty(nameof(Paciente.Persona.ComplementoDocumento), nameof(PacienteResponseDto.ComplementoDocumento))]
    [MapProperty(nameof(Paciente.Persona.ExtensionDocumentoId), nameof(PacienteResponseDto.ExtensionDocumentoId))]
    [MapProperty(nameof(Paciente.Persona.FechaNacimiento), nameof(PacienteResponseDto.FechaNacimiento))]
    [MapProperty(nameof(Paciente.Persona.SexoId), nameof(PacienteResponseDto.SexoId))]
    [MapProperty(nameof(Paciente.Persona.EstadoCivilId), nameof(PacienteResponseDto.EstadoCivilId))]
    [MapProperty(nameof(Paciente.Persona.Telefono), nameof(PacienteResponseDto.Telefono))]
    [MapProperty(nameof(Paciente.Persona.Direccion), nameof(PacienteResponseDto.Direccion))]
    [MapperIgnoreTarget(nameof(PacienteResponseDto.TipoDocumento))]
    [MapperIgnoreTarget(nameof(PacienteResponseDto.ExtensionDocumento))]
    [MapperIgnoreTarget(nameof(PacienteResponseDto.Sexo))]
    [MapperIgnoreTarget(nameof(PacienteResponseDto.EstadoCivil))]
    [MapperIgnoreTarget(nameof(PacienteResponseDto.Edad))]
    public partial PacienteResponseDto ToDto(Paciente entity);
}
