using Hospital.Domain.Common;

namespace Hospital.Domain.Entities;

public class CatalogoItem : AuditableEntity
{
    public int CatalogoGrupoId { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Valor { get; set; } = string.Empty;
    public int Orden { get; set; }
    public CatalogoGrupo CatalogoGrupo { get; set; } = null!;
}