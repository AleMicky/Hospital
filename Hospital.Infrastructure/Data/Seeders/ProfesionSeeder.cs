using Hospital.Domain.Entities.Catalogos;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Data.Seeders;

public class ProfesionSeeder(AppDbContext context) : ISeeder
{
    public async Task SeedAsync(CancellationToken cancellationToken)
    {
        var profesiones = new[]
        {
            ("MED_GENERAL","Médico General"),
            ("CARDIOLOGO","Cardiólogo"),
            ("GINECOLOGO","Ginecólogo"),
            ("PEDIATRA","Pediatra"),
            ("TRAUMATOLOGO","Traumatólogo"),
            ("ANESTESIOLOGO","Anestesiólogo"),
            ("BIOQUIMICA","Bioquímica"),
            ("LIC_ENFERMERIA","Licenciada en Enfermería"),
            ("TEC_RADIOLOGO","Técnico Radiólogo")
        };

        foreach (var (codigo,nombre) in profesiones)
        {
            if (await context.Profesiones.AnyAsync(
                    x => x.Codigo == codigo,
                    cancellationToken))
                continue;

            context.Profesiones.Add(new Profesion
            {
                Codigo = codigo,
                Nombre = nombre
            });
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}