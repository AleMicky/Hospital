using Hospital.Domain.Common;
using Hospital.Domain.Enums;

namespace Hospital.Domain.Entities;

public class FormularioCampo : AuditableEntity
{
    public int FormularioId { get; set; }
    public Formulario Formulario { get; set; } = null!;
    public string NombreCampo { get; set; } = string.Empty;
    public string Etiqueta { get; set; } = string.Empty;
    public string Seccion { get; set; } = string.Empty;
    public TipoDatoCampo TipoDato { get; set; }
    public TipoControlCampo TipoControl { get; set; }
    public bool EsRequerido { get; set; }
    public string Placeholder { get; set; } = string.Empty;
    public string ValorDefault { get; set; } = string.Empty;
    public int Orden { get; set; }
    public int? LongitudMaxima { get; set; }
    public decimal? Minimo { get; set; }
    public decimal? Maximo { get; set; }
    public string OpcionesJson { get; set; } = string.Empty;
    public ICollection<AtencionValor> Valores { get; set; } = new List<AtencionValor>();
}