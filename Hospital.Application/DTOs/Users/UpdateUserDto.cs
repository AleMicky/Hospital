namespace Hospital.Application.DTOs.Users;

public class UpdateUserDto
{
    public string NombreCompleto { get; set; } = string.Empty;
    public string Rol { get; set; } = string.Empty;
    public string? Password { get; set; }
    public bool Activo { get; set; } = true;
}
