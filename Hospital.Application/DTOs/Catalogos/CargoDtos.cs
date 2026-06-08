namespace Hospital.Application.DTOs.Catalogos;

public sealed record CargoResponseDto(
    int Id,
    string Codigo,
    string Nombre,
    string? Descripcion
);

public sealed record CreateCargoDto(
    string Codigo,
    string Nombre,
    string? Descripcion
);

public sealed record UpdateCargoDto(
    string Codigo,
    string Nombre,
    string? Descripcion
);
