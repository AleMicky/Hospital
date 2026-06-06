namespace Hospital.Application.DTOs.TiposAtencion;

public sealed record CreateTipoAtencionDto(
    string Codigo,
    string Nombre,
    string Descripcion,
    int Orden,
    string Color,
    string Icono
);
