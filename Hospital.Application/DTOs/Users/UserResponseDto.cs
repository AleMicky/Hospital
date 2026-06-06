namespace Hospital.Application.DTOs.Users;

public class UserResponseDto
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string NombreCompleto { get; set; } = string.Empty;
    public bool Activo { get; set; }
    public List<string> Roles { get; set; } = new();
}