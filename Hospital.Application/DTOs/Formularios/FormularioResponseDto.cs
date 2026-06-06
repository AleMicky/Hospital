namespace Hospital.Application.DTOs.Formularios;

public sealed record FormularioResponseDto(
    int Id,
    int TipoAtencionId,
    string Nombre,
    string Descripcion,
    int Version,
    bool EsPlantilla
);
