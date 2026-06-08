using Hospital.Domain.Entities.Catalogos;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Data.Seeders;

public class CargoSeeder(AppDbContext context) : ISeeder
{
    public async Task SeedAsync(CancellationToken cancellationToken)
    {
        var cargos = new[]
        {
            ("DIRECTOR_GENERAL","Director General"),
            ("DIRECTOR_MEDICO","Director Médico"),
            ("MEDICO_GUARDIA","Médico de Guardia"),
            ("JEFE_ENFERMERIA","Jefa de Enfermería"),
            ("AUXILIAR_ENFERMERIA","Auxiliar de Enfermería"),
            ("RADIOLOGO","Radiólogo"),
            ("ECOGRAFISTA","Ecografista"),
            ("RESPONSABLE_FARMACIA","Responsable Farmacia")
        };

        foreach (var (codigo,nombre) in cargos)
        {
            if (await context.Cargos.AnyAsync(
                    x => x.Codigo == codigo,
                    cancellationToken))
                continue;

            context.Cargos.Add(new Cargo
            {
                Codigo = codigo,
                Nombre = nombre
            });
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}