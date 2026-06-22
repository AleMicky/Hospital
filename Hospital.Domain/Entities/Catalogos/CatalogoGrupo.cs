using Hospital.Domain.Common;

namespace Hospital.Domain.Entities;

public class CatalogoGrupo : AuditableEntity
{
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public ICollection<CatalogoItem> Items { get; set; } = new List<CatalogoItem>();
}