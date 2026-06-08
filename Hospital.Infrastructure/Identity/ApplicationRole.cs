using Microsoft.AspNetCore.Identity;

namespace Hospital.Infrastructure.Identity;

public class ApplicationRole : IdentityRole<int>
{
    public string Descripcion { get; set; } = string.Empty;
    public bool Activo { get; set; } = true;
}