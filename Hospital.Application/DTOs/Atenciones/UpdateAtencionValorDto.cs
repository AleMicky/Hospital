namespace Hospital.Application.DTOs.Atenciones;

public sealed record UpdateAtencionValorDto(
    string? ValorTexto,
    int? ValorNumero,
    decimal? ValorDecimal,
    DateOnly? ValorFecha,
    bool? ValorBoolean
);
