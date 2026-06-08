namespace Hospital.Domain.Entities;

 
public class RolPermiso
{
    public int RoleId { get; set; }
   // public ApplicationRole Role { get; set; } = null!;

    public int PermisoId { get; set; }
    public Permiso Permiso { get; set; } = null!;
}