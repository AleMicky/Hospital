namespace Hospital.Application.DTOs.Catalogos;

public sealed record ServicioResponseDto(
    int Id,
    int DepartamentoId,
    string DepartamentoNombre,
    string Codigo,
    string Nombre,
    string? Descripcion
);

public sealed record CreateServicioDto(
    int DepartamentoId,
    string Codigo,
    string Nombre,
    string? Descripcion
);

public sealed record UpdateServicioDto(
    int DepartamentoId,
    string Codigo,
    string Nombre,
    string? Descripcion
);
