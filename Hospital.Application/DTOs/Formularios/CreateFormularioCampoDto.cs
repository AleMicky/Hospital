using Hospital.Domain.Enums;

namespace Hospital.Application.DTOs.Formularios;

public sealed record CreateFormularioCampoDto(
    int FormularioId,
    string NombreCampo,
    string Etiqueta,
    string Seccion,
    TipoDatoCampo TipoDato,
    TipoControlCampo TipoControl,
    bool EsRequerido,
    string Placeholder,
    string ValorDefault,
    int Orden,
    int? LongitudMaxima,
    decimal? Minimo,
    decimal? Maximo,
    string OpcionesJson
);
