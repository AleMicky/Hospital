using Hospital.Domain.Common;

namespace Hospital.Domain.Entities.Catalogos;

public class Servicio : AuditableEntity
{
    public int Id { get; set; }
    public int DepartamentoId { get; set; }
    public Departamento Departamento { get; set; } = null!;
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public ICollection<Prestacion> Prestaciones { get; set; } = [];
}