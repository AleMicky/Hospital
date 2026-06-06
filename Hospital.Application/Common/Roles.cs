namespace Hospital.Application.Common;

public static class AppRoles
{
    public const string Admin = "Admin";
    public const string Medico = "Medico";
    public const string Recepcion = "Recepcion";

    public static readonly string[] All = [Admin, Medico, Recepcion];
}
