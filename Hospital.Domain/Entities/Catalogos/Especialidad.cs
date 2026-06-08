using Hospital.Domain.Common;

namespace Hospital.Domain.Entities.Catalogos;

public class Especialidad : AuditableEntity
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
}