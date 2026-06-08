namespace Hospital.Application.DTOs.Catalogos;

public sealed record EspecialidadResponseDto(
    int Id,
    string Codigo,
    string Nombre,
    string? Descripcion
);

public sealed record CreateEspecialidadDto(
    string Codigo,
    string Nombre,
    string? Descripcion
);

public sealed record UpdateEspecialidadDto(
    string Codigo,
    string Nombre,
    string? Descripcion
);
