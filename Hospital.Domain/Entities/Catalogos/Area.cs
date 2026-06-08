using Hospital.Domain.Common;

namespace Hospital.Domain.Entities.Catalogos;

public class Area : AuditableEntity
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }

    public ICollection<Departamento> Departamentos { get; set; } = [];
}