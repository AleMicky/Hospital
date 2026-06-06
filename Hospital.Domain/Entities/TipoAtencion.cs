using Hospital.Domain.Common;

namespace Hospital.Domain.Entities;

public class TipoAtencion : AuditableEntity
{
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public int Orden { get; set; }
    public string Color { get; set; } = string.Empty;
    public string Icono { get; set; } = string.Empty;
    public ICollection<Formulario> Formularios { get; set; } = new List<Formulario>();
    public ICollection<Atencion> Atenciones { get; set; } = new List<Atencion>();
}