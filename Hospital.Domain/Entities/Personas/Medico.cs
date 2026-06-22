using Hospital.Domain.Common;
using Hospital.Domain.Entities.Catalogos;

namespace Hospital.Domain.Entities.Personas;

public class Medico : AuditableEntity
{
    public int EmpleadoId { get; set; }
    public Empleado Empleado { get; set; } = null!;

    public int EspecialidadId { get; set; }
    public Especialidad Especialidad { get; set; } = null!;

    public string? MatriculaProfesional { get; set; }
}
