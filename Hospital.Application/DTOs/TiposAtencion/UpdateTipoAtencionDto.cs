namespace Hospital.Application.DTOs.TiposAtencion;

public sealed record UpdateTipoAtencionDto(
    string Nombre,
    string Descripcion,
    int Orden,
    string Color,
    string Icono
);
