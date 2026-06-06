namespace Hospital.Application.DTOs.Pacientes;

public sealed record CreatePacienteDto(
    string CodigoPaciente,
    string Nombres,
    string ApellidoPaterno,
    string ApellidoMaterno,
    int TipoDocumentoId,
    string NumeroDocumento,
    string? ComplementoDocumento,
    int? ExtensionDocumentoId,
    DateOnly FechaNacimiento,
    int SexoId,
    int EstadoCivilId,
    string Telefono,
    string Direccion,
    string? OcupacionProfesion
);