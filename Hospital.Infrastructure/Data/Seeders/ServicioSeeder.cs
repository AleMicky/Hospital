using Hospital.Domain.Entities.Catalogos;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Data.Seeders;

public class ServicioSeeder(AppDbContext context) : ISeeder
{
    public async Task SeedAsync(CancellationToken cancellationToken)
    {
        var medico = await context.Departamentos
            .FirstAsync(x => x.Codigo == "MED_ASISTENCIAL", cancellationToken);

        var enfermeria = await context.Departamentos
            .FirstAsync(x => x.Codigo == "ENFERMERIA", cancellationToken);

        var apoyo = await context.Departamentos
            .FirstAsync(x => x.Codigo == "APOYO_DIAGNOSTICO", cancellationToken);

        await CreateAsync(medico.Id, "MED_GUARDIA", "Médico de Guardia", cancellationToken);
        await CreateAsync(medico.Id, "CONS_EXT", "Consulta Externa", cancellationToken);
        await CreateAsync(medico.Id, "QUIROFANO", "Quirófano", cancellationToken);

        await CreateAsync(enfermeria.Id, "JEFATURA", "Jefatura", cancellationToken);
        await CreateAsync(enfermeria.Id, "LIC_ENFERMERIA", "Licenciadas en Enfermería", cancellationToken);
        await CreateAsync(enfermeria.Id, "TEC_ENFERMERIA", "Técnico Medio en Enfermería", cancellationToken);

        await CreateAsync(apoyo.Id, "LABORATORIO", "Laboratorio", cancellationToken);
        await CreateAsync(apoyo.Id, "RAYOS_X", "Rayos X", cancellationToken);
        await CreateAsync(apoyo.Id, "FARMACIA", "Farmacia", cancellationToken);
        await CreateAsync(apoyo.Id, "ECOGRAFIA", "Ecografía", cancellationToken);
    }

    private async Task CreateAsync(
        int departamentoId,
        string codigo,
        string nombre,
        CancellationToken cancellationToken)
    {
        if (await context.Servicios.AnyAsync(
                x => x.Codigo == codigo,
                cancellationToken))
            return;

        context.Servicios.Add(new Servicio
        {
            DepartamentoId = departamentoId,
            Codigo = codigo,
            Nombre = nombre
        });

        await context.SaveChangesAsync(cancellationToken);
    }
}