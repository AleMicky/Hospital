using Hospital.Domain.Common;

namespace Hospital.Domain.Entities;

public class AtencionValor : AuditableEntity
{
    public int AtencionId { get; set; }
    public Atencion Atencion { get; set; } = null!;
    public int FormularioCampoId { get; set; }
    public FormularioCampo FormularioCampo { get; set; } = null!;
    public string? ValorTexto { get; set; }
    public int? ValorNumero { get; set; }
    public decimal? ValorDecimal { get; set; }
    public DateOnly? ValorFecha { get; set; }
    public bool? ValorBoolean { get; set; }
}