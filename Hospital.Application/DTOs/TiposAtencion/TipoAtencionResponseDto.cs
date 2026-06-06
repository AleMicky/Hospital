namespace Hospital.Application.DTOs.TiposAtencion;

public sealed record TipoAtencionResponseDto(
    int Id,
    string Codigo,
    string Nombre,
    string Descripcion,
    int Orden,
    string Color,
    string Icono
);
