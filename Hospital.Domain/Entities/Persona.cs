using Hospital.Domain.Common;

namespace Hospital.Domain.Entities;

public class Persona : AuditableEntity
{
    public string Nombres { get; set; } = string.Empty;
    public string ApellidoPaterno { get; set; } = string.Empty;
    public string ApellidoMaterno { get; set; } = string.Empty;
    public int TipoDocumentoId { get; set; }
    public CatalogoItem TipoDocumento { get; set; } = null!;
    public string NumeroDocumento { get; set; } = string.Empty;
    public string? ComplementoDocumento { get; set; }
    public int? ExtensionDocumentoId { get; set; }
    public CatalogoItem? ExtensionDocumento { get; set; }
    public DateOnly FechaNacimiento { get; set; }
}
/*
 Seguridad
   ├── Usuarios
   ├── Roles
   ├── Permisos
   
   Personas
   ├── Personas
   ├── Pacientes
   ├── Médicos
   ├── Empleados
   
   Atenciones
   ├── TiposAtencion
   ├── Atenciones
   ├── EstadosAtencion
   ├── Triaje
   ├── Diagnósticos
   ├── Procedimientos
   
   Agenda
   ├── Consultorios
   ├── HorariosMedicos
   ├── CitasMedicas
   
   Caja
   ├── Servicios
   ├── Tarifas
   ├── OrdenesPago
   ├── Pagos
   ├── DetallePago
   
   Internación
   ├── Camas
   ├── Habitaciones
   ├── Internaciones
   ├── MovimientosCama
   
   Farmacia / Inventario
   ├── Productos
   ├── CategoriasProducto
   ├── Almacenes
   ├── Stock
   ├── MovimientosStock
   
   Catálogos
   ├── CatalogoGrupos
   ├── CatalogoItems
   
   Workflow
   ├── WorkflowDefinitions
   ├── WorkflowStates
   ├── WorkflowTransitions
   ├── WorkflowInstances
   ├── WorkflowHistories
   
   Auditoría
   ├── Logs
   ├── HistorialCambios
 */