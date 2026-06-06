namespace Hospital.Application.DTOs.Formularios;

public sealed record UpdateFormularioDto(
    string Nombre,
    string Descripcion,
    int Version,
    bool EsPlantilla
);
