using Hospital.Domain.Entities.Catalogos;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Data.Seeders;

public class EspecialidadSeeder(AppDbContext context) : ISeeder
{
    public async Task SeedAsync(CancellationToken cancellationToken)
    {
        var especialidades = new[]
        {
            ("CARDIOLOGIA","Cardiología"),
            ("PEDIATRIA","Pediatría"),
            ("GINECOLOGIA","Ginecología"),
            ("TRAUMATOLOGIA","Traumatología"),
            ("UROLOGIA","Urología"),
            ("PSIQUIATRIA","Psiquiatría"),
            ("MEDICINA_INTERNA","Medicina Interna")
        };

        foreach (var (codigo,nombre) in especialidades)
        {
            if (await context.Especialidades.AnyAsync(
                    x => x.Codigo == codigo,
                    cancellationToken))
                continue;

            context.Especialidades.Add(new Especialidad
            {
                Codigo = codigo,
                Nombre = nombre
            });
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}