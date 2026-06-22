using Hospital.Domain.Entities.Personas;
using Hospital.Infrastructure.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Configurations.Personas;

public class EmpleadoConfiguration : IEntityTypeConfiguration<Empleado>
{
    public void Configure(EntityTypeBuilder<Empleado> builder)
    {
        builder.ToTable("Empleados", "Personas");
        builder.ConfigureAuditableEntity();

        builder.HasOne(x => x.Persona)
            .WithMany()
            .HasForeignKey(x => x.PersonaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Area)
            .WithMany()
            .HasForeignKey(x => x.AreaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Departamento)
            .WithMany()
            .HasForeignKey(x => x.DepartamentoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Servicio)
            .WithMany()
            .HasForeignKey(x => x.ServicioId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Profesion)
            .WithMany()
            .HasForeignKey(x => x.ProfesionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Cargo)
            .WithMany()
            .HasForeignKey(x => x.CargoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
