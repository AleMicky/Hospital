using Hospital.Domain.Entities.Catalogos;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Data.Seeders;

public class DepartamentoSeeder(AppDbContext context) : ISeeder
{
    public async Task SeedAsync(CancellationToken cancellationToken)
    {
        var admin = await context.Areas
            .FirstAsync(x => x.Codigo == "ADMIN", cancellationToken);

        var salud = await context.Areas
            .FirstAsync(x => x.Codigo == "ATENCION_SALUD", cancellationToken);

        await CreateAsync(admin.Id,
            "ADMINISTRACION",
            "Administración",
            cancellationToken);

        await CreateAsync(salud.Id,
            "MED_ASISTENCIAL",
            "Médico Asistencial",
            cancellationToken);

        await CreateAsync(salud.Id,
            "ENFERMERIA",
            "Enfermería",
            cancellationToken);

        await CreateAsync(salud.Id,
            "APOYO_DIAGNOSTICO",
            "Apoyo al Diagnóstico",
            cancellationToken);
    }

    private async Task CreateAsync(
        int areaId,
        string codigo,
        string nombre,
        CancellationToken cancellationToken)
    {
        if (await context.Departamentos.AnyAsync(
                x => x.Codigo == codigo,
                cancellationToken))
            return;

        context.Departamentos.Add(new Departamento
        {
            AreaId = areaId,
            Codigo = codigo,
            Nombre = nombre
        });

        await context.SaveChangesAsync(cancellationToken);
    }
}