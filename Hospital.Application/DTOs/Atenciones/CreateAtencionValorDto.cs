namespace Hospital.Application.DTOs.Atenciones;

public sealed record CreateAtencionValorDto(
    int AtencionId,
    int FormularioCampoId,
    string? ValorTexto,
    int? ValorNumero,
    decimal? ValorDecimal,
    DateOnly? ValorFecha,
    bool? ValorBoolean
);
