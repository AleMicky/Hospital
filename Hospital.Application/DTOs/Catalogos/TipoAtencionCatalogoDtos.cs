namespace Hospital.Application.DTOs.Catalogos;

public sealed record TipoAtencionCatalogoResponseDto(
    int Id,
    string Codigo,
    string Nombre,
    string? Descripcion
);

public sealed record CreateTipoAtencionCatalogoDto(
    string Codigo,
    string Nombre,
    string? Descripcion
);

public sealed record UpdateTipoAtencionCatalogoDto(
    string Codigo,
    string Nombre,
    string? Descripcion
);
