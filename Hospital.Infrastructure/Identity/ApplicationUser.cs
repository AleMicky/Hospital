using Microsoft.AspNetCore.Identity;

namespace Hospital.Infrastructure.Identity;

public class ApplicationUser : IdentityUser<int>
{
    public string NombreCompleto { get; set; } = string.Empty;
    public bool Activo { get; set; } = true;
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiresAt { get; set; }
}