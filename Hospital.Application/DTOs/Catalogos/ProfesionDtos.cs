namespace Hospital.Application.DTOs.Catalogos;

public sealed record ProfesionResponseDto(
    int Id,
    string Codigo,
    string Nombre,
    string? Descripcion
);

public sealed record CreateProfesionDto(
    string Codigo,
    string Nombre,
    string? Descripcion
);

public sealed record UpdateProfesionDto(
    string Codigo,
    string Nombre,
    string? Descripcion
);
