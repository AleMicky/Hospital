namespace Hospital.Application.DTOs.Atenciones;

public sealed record CreateAtencionDto(
    int PacienteId,
    int TipoAtencionId,
    int FormularioId,
    DateOnly Fecha,
    TimeOnly Hora,
    string Estado,
    string MotivoConsulta,
    int? ProfesionalId
);
