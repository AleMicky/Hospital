using Hospital.Domain.Common;

namespace Hospital.Domain.Entities.Catalogos;

public class Departamento : AuditableEntity
{
    public int Id { get; set; }
    public int AreaId { get; set; }
    public Area Area { get; set; } = null!;
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public ICollection<Servicio> Servicios { get; set; } = [];
}