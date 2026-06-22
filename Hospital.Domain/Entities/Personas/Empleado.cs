using Hospital.Domain.Common;
using Hospital.Domain.Entities.Catalogos;

namespace Hospital.Domain.Entities.Personas;

public class Empleado : AuditableEntity
{
    public int PersonaId { get; set; }
    public Persona Persona { get; set; } = null!;

    public int AreaId { get; set; }
    public Area Area { get; set; } = null!;

    public int DepartamentoId { get; set; }
    public Departamento Departamento { get; set; } = null!;

    public int ServicioId { get; set; }
    public Servicio Servicio { get; set; } = null!;

    public int ProfesionId { get; set; }
    public Profesion Profesion { get; set; } = null!;

    public int CargoId { get; set; }
    public Cargo Cargo { get; set; } = null!;
}
