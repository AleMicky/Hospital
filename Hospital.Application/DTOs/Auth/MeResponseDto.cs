namespace Hospital.Application.DTOs.Auth;

public class MeResponseDto
{
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string NombreCompleto { get; set; } = string.Empty;
    public List<string> Roles { get; set; } = new();
}