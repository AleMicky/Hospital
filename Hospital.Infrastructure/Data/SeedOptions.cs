namespace Hospital.Infrastructure.Data;

public class SeedOptions
{
    public const string SectionName = "Seed";
    public string AdminUserName { get; set; } = "admin";
    public string AdminPassword { get; set; } = "Admin123!";
    public string AdminNombreCompleto { get; set; } = "Administrador del Sistema";
}