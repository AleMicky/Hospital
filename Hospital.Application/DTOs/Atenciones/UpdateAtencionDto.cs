namespace Hospital.Application.DTOs.Atenciones;

public sealed record UpdateAtencionDto(
    DateOnly Fecha,
    TimeOnly Hora,
    string Estado,
    string MotivoConsulta,
    int? ProfesionalId
);
