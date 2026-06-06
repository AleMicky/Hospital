using Hospital.Domain.Common;

namespace Hospital.Domain.Entities;

public class Paciente : AuditableEntity
{
    public string CodigoPaciente { get; set; } = string.Empty;

    public string Nombres { get; set; } = string.Empty;
    public string ApellidoPaterno { get; set; } = string.Empty;
    public string ApellidoMaterno { get; set; } = string.Empty;

    public int TipoDocumentoId { get; set; }
    public string NumeroDocumento { get; set; } = string.Empty;
    public string? ComplementoDocumento { get; set; }
    public int? ExtensionDocumentoId { get; set; }

    public DateOnly FechaNacimiento { get; set; }

    public int SexoId { get; set; }
    public int EstadoCivilId { get; set; }

    public string Telefono { get; set; } = string.Empty;
    public string Direccion { get; set; } = string.Empty;
    public string? OcupacionProfesion { get; set; }

    public CatalogoItem TipoDocumento { get; set; } = null!;
    public CatalogoItem? ExtensionDocumento { get; set; }
    public CatalogoItem Sexo { get; set; } = null!;
    public CatalogoItem EstadoCivil { get; set; } = null!;

    public ICollection<Atencion> Atenciones { get; set; } = [];
}