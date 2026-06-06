namespace Hospital.Application.DTOs.Formularios;

public sealed record CreateFormularioDto(
    int TipoAtencionId,
    string Nombre,
    string Descripcion,
    int Version,
    bool EsPlantilla
);
