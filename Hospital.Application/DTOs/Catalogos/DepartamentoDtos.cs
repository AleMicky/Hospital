namespace Hospital.Application.DTOs.Catalogos;

public sealed record DepartamentoResponseDto(
    int Id,
    int AreaId,
    string AreaNombre,
    string Codigo,
    string Nombre,
    string? Descripcion
);

public sealed record CreateDepartamentoDto(
    int AreaId,
    string Codigo,
    string Nombre,
    string? Descripcion
);

public sealed record UpdateDepartamentoDto(
    int AreaId,
    string Codigo,
    string Nombre,
    string? Descripcion
);
