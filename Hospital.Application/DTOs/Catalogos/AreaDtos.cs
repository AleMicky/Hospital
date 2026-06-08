namespace Hospital.Application.DTOs.Catalogos;

public sealed record AreaResponseDto(
    int Id,
    string Codigo,
    string Nombre,
    string? Descripcion
);

public sealed record CreateAreaDto(
    string Codigo,
    string Nombre,
    string? Descripcion
);

public sealed record UpdateAreaDto(
    string Codigo,
    string Nombre,
    string? Descripcion
);
