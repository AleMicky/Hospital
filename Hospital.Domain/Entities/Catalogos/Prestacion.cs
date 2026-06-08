using Hospital.Domain.Common;

namespace Hospital.Domain.Entities.Catalogos;

public class Prestacion : AuditableEntity
{
    public int Id { get; set; }
    public int ServicioId { get; set; }
    public Servicio Servicio { get; set; } = null!;
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public decimal Precio { get; set; }
    public bool RequiereOrdenMedica { get; set; }
    public bool RequiereMedico { get; set; }
}