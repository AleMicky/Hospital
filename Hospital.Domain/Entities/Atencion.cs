using Hospital.Domain.Common;

namespace Hospital.Domain.Entities;

public class Atencion : AuditableEntity
{
    public int PacienteId { get; set; }
    public Paciente Paciente { get; set; } = null!;
    public int TipoAtencionId { get; set; }
    public TipoAtencion TipoAtencion { get; set; } = null!;
    public int FormularioId { get; set; }
    public Formulario Formulario { get; set; } = null!;
    public DateOnly Fecha { get; set; }
    public TimeOnly Hora { get; set; }
    public string Estado { get; set; } = "Abierta";
    public string MotivoConsulta { get; set; } = string.Empty;
    public int? ProfesionalId { get; set; }
    public ICollection<AtencionValor> Valores { get; set; } = new List<AtencionValor>();
}