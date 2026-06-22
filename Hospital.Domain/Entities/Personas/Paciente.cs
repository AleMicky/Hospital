using Hospital.Domain.Common;

namespace Hospital.Domain.Entities.Personas;

public class Paciente : AuditableEntity
{
    public int PersonaId { get; set; }
    public Persona Persona { get; set; } = null!;

    public string CodigoPaciente { get; set; } = string.Empty;
    public string? OcupacionProfesion { get; set; }
}
