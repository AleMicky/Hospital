namespace Hospital.Application.DTOs.Pacientes;

public sealed record PacienteResponseDto
{
    public int Id { get; init; }
    public string CodigoPaciente { get; init; } = string.Empty;
    public string Nombres { get; init; } = string.Empty;
    public string ApellidoPaterno { get; init; } = string.Empty;
    public string ApellidoMaterno { get; init; } = string.Empty;

    public string NombreCompleto =>
        $"{Nombres} {ApellidoPaterno} {ApellidoMaterno}"
            .Replace("  ", " ")
            .Trim();

    public int TipoDocumentoId { get; init; }
    public string TipoDocumento { get; init; } = string.Empty;

    public string NumeroDocumento { get; init; } = string.Empty;
    public string? ComplementoDocumento { get; init; }

    public int? ExtensionDocumentoId { get; init; }
    public string? ExtensionDocumento { get; init; }

    public string DocumentoCompleto =>
        string.IsNullOrWhiteSpace(ComplementoDocumento)
            ? NumeroDocumento
            : $"{NumeroDocumento}-{ComplementoDocumento}";

    public DateOnly FechaNacimiento { get; init; }
    public int Edad { get; init; }

    public int SexoId { get; init; }
    public string Sexo { get; init; } = string.Empty;

    public int EstadoCivilId { get; init; }
    public string EstadoCivil { get; init; } = string.Empty;

    public string Telefono { get; init; } = string.Empty;
    public string Direccion { get; init; } = string.Empty;
    public string? OcupacionProfesion { get; init; }

    public bool Activo { get; init; }
}