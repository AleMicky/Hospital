using Hospital.Domain.Entities.Personas;
using Hospital.Infrastructure.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Configurations.Personas;

public class MedicoConfiguration : IEntityTypeConfiguration<Medico>
{
    public void Configure(EntityTypeBuilder<Medico> builder)
    {
        builder.ToTable("Medicos", "Personas");
        builder.ConfigureAuditableEntity();

        builder.Property(x => x.MatriculaProfesional)
            .HasMaxLength(50);

        builder.HasOne(x => x.Empleado)
            .WithMany()
            .HasForeignKey(x => x.EmpleadoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Especialidad)
            .WithMany()
            .HasForeignKey(x => x.EspecialidadId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
