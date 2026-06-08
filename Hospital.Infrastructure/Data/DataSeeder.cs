using Hospital.Application.Common;
using Hospital.Application.Interfaces;
using Hospital.Domain.Entities;
using Hospital.Infrastructure.Data.Seeders;
using Hospital.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Hospital.Infrastructure.Data;

public class DataSeeder(
    AppDbContext context,
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole<int>> roleManager,
    IOptions<SeedOptions> seedOptions,
    ILogger<DataSeeder> logger) : IDataSeeder
{
    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        await SeedRolesAsync(cancellationToken);
        await SeedAdminUserAsync(cancellationToken);
        await SeedCatalogosBasicosAsync(cancellationToken);
        await SeedCatalogosClinicaAsync(cancellationToken);
    }

    private async Task SeedRolesAsync(CancellationToken cancellationToken)
    {
        foreach (var roleName in AppRoles.All)
        {
            if (await roleManager.RoleExistsAsync(roleName))
                continue;

            await roleManager.CreateAsync(new IdentityRole<int>(roleName));
            logger.LogInformation("Rol creado: {Role}", roleName);
        }
    }

    private async Task SeedAdminUserAsync(CancellationToken cancellationToken)
    {
        var options = seedOptions.Value;
        var admin = await userManager.FindByNameAsync(options.AdminUserName);

        if (admin is not null)
            return;

        admin = new ApplicationUser
        {
            UserName = options.AdminUserName,
            NombreCompleto = options.AdminNombreCompleto,
            Activo = true,
            Email = $"{options.AdminUserName}@hospital.local",
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(admin, options.AdminPassword);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            logger.LogError("No se pudo crear el usuario admin: {Errors}", errors);
            return;
        }

        await userManager.AddToRoleAsync(admin, AppRoles.Admin);

        logger.LogInformation(
            "Usuario admin creado: {UserName}. Cambia la contraseña en producción.",
            options.AdminUserName);
    }

    private async Task SeedCatalogosClinicaAsync(CancellationToken cancellationToken)
    {
        ISeeder[] seeders =
        [
            new AreaSeeder(context),
            new DepartamentoSeeder(context),
            new ServicioSeeder(context),
            new ProfesionSeeder(context),
            new EspecialidadSeeder(context),
            new CargoSeeder(context)
        ];

        foreach (var seeder in seeders)
        {
            await seeder.SeedAsync(cancellationToken);
        }
    }

    private async Task SeedCatalogosBasicosAsync(CancellationToken cancellationToken)
    {
        await SeedCatalogoGrupoAsync(
            "SEXO",
            "Sexo",
            "Catálogo de sexo",
            [
                ("M", "Masculino", "M", 1),
                ("F", "Femenino", "F", 2)
            ],
            cancellationToken);

        await SeedCatalogoGrupoAsync(
            "ESTADO_CIVIL",
            "Estado civil",
            "Estado civil del paciente",
            [
                ("SOLTERO", "Soltero/a", "SOLTERO", 1),
                ("CASADO", "Casado/a", "CASADO", 2),
                ("DIVORCIADO", "Divorciado/a", "DIVORCIADO", 3),
                ("VIUDO", "Viudo/a", "VIUDO", 4)
            ],
            cancellationToken);

        await SeedCatalogoGrupoAsync(
            "TIPO_DOCUMENTO",
            "Tipo de Documento",
            "Tipos de documentos de identificación",
            [
                ("CI", "Carnet de Identidad", "CI", 1),
                ("PASAPORTE", "Pasaporte", "PASAPORTE", 2),
                ("OTRO", "Otro", "OTRO", 3)
            ],
            cancellationToken);

        await SeedCatalogoGrupoAsync(
            "EXTENSION_DOCUMENTO",
            "Extensión de Documento",
            "Extensiones departamentales de Bolivia",
            [
                ("LP", "La Paz", "LP", 1),
                ("CB", "Cochabamba", "CB", 2),
                ("SC", "Santa Cruz", "SC", 3),
                ("OR", "Oruro", "OR", 4),
                ("PT", "Potosí", "PT", 5),
                ("TJ", "Tarija", "TJ", 6),
                ("CH", "Chuquisaca", "CH", 7),
                ("BN", "Beni", "BN", 8),
                ("PD", "Pando", "PD", 9)
            ],
            cancellationToken);
    }

    private async Task SeedCatalogoGrupoAsync(
        string codigo,
        string nombre,
        string descripcion,
        (string Codigo, string Nombre, string Valor, int Orden)[] items,
        CancellationToken cancellationToken)
    {
        var grupo = await context.CatalogoGrupos
            .FirstOrDefaultAsync(x => x.Codigo == codigo, cancellationToken);

        if (grupo is null)
        {
            grupo = new CatalogoGrupo
            {
                Codigo = codigo,
                Nombre = nombre,
                Descripcion = descripcion,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "Sistema",
                Activo = true
            };

            context.CatalogoGrupos.Add(grupo);
            await context.SaveChangesAsync(cancellationToken);

            logger.LogInformation("Catálogo creado: {Codigo}", codigo);
        }

        foreach (var item in items)
        {
            var exists = await context.CatalogoItems.AnyAsync(
                x => x.CatalogoGrupoId == grupo.Id && x.Codigo == item.Codigo,
                cancellationToken);

            if (exists)
                continue;

            context.CatalogoItems.Add(new CatalogoItem
            {
                CatalogoGrupoId = grupo.Id,
                Codigo = item.Codigo,
                Nombre = item.Nombre,
                Valor = item.Valor,
                Orden = item.Orden,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "Sistema",
                Activo = true
            });
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}