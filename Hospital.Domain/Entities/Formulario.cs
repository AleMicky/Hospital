using Hospital.Domain.Common;

namespace Hospital.Domain.Entities;

public class Formulario : AuditableEntity
{
    public int TipoAtencionId { get; set; }
    public TipoAtencion TipoAtencion { get; set; } = null!;
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public int Version { get; set; } = 1;
    public bool EsPlantilla { get; set; } = true;
    public ICollection<FormularioCampo> Campos { get; set; } = new List<FormularioCampo>();
    public ICollection<Atencion> Atenciones { get; set; } = new List<Atencion>();
}