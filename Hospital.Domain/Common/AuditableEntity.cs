namespace Hospital.Domain.Common;

public class AuditableEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public bool Activo { get; set; } = true;
}