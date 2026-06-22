using Hospital.Domain.Common;
using Hospital.Domain.Entities;

namespace Hospital.Domain.Entities.Personas;

public class Persona : AuditableEntity
{
    public string Nombres { get; set; } = string.Empty;
    public string ApellidoPaterno { get; set; } = string.Empty;
    public string ApellidoMaterno { get; set; } = string.Empty;
    public DateOnly FechaNacimiento { get; set; }
    public string Telefono { get; set; } = string.Empty;
    public string Direccion { get; set; } = string.Empty;

    public int TipoDocumentoId { get; set; }
    public CatalogoItem TipoDocumento { get; set; } = null!;
    public string NumeroDocumento { get; set; } = string.Empty;

    public int? ExtensionDocumentoId { get; set; }
    public CatalogoItem? ExtensionDocumento { get; set; }
    public string? ComplementoDocumento { get; set; }

    public int SexoId { get; set; }
    public CatalogoItem Sexo { get; set; } = null!;

    public int EstadoCivilId { get; set; }
    public CatalogoItem EstadoCivil { get; set; } = null!;
}
