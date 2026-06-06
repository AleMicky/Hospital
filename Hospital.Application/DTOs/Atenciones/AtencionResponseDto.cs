namespace Hospital.Application.DTOs.Atenciones;

public sealed record AtencionResponseDto(
    int Id,
    int PacienteId,
    int TipoAtencionId,
    int FormularioId,
    DateOnly Fecha,
    TimeOnly Hora,
    string Estado,
    string MotivoConsulta,
    int? ProfesionalId
);
