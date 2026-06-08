using Hospital.Domain.Entities.Catalogos;
using Hospital.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Data.Seeders;

public class AreaSeeder(AppDbContext context) : ISeeder
{
    public async Task SeedAsync(CancellationToken cancellationToken)
    {
        await CreateAsync("ADMIN", "Administración", cancellationToken);
        await CreateAsync("ATENCION_SALUD", "Atención en Salud", cancellationToken);
    }

    private async Task CreateAsync(
        string codigo,
        string nombre,
        CancellationToken cancellationToken)
    {
        if (await context.Areas.AnyAsync(
                x => x.Codigo == codigo,
                cancellationToken))
            return;

        context.Areas.Add(new Area
        {
            Codigo = codigo,
            Nombre = nombre
        });

        await context.SaveChangesAsync(cancellationToken);
    }
}