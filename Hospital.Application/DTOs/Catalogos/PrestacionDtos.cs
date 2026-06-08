namespace Hospital.Application.DTOs.Catalogos;

public sealed record PrestacionResponseDto(
    int Id,
    int ServicioId,
    string ServicioNombre,
    string Codigo,
    string Nombre,
    string? Descripcion,
    decimal Precio,
    bool RequiereOrdenMedica,
    bool RequiereMedico
);

public sealed record CreatePrestacionDto(
    int ServicioId,
    string Codigo,
    string Nombre,
    string? Descripcion,
    decimal Precio,
    bool RequiereOrdenMedica,
    bool RequiereMedico
);

public sealed record UpdatePrestacionDto(
    int ServicioId,
    string Codigo,
    string Nombre,
    string? Descripcion,
    decimal Precio,
    bool RequiereOrdenMedica,
    bool RequiereMedico
);
