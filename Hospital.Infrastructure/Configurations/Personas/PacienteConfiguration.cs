using Hospital.Domain.Entities.Personas;
using Hospital.Infrastructure.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Configurations.Personas;

public class PacienteConfiguration : IEntityTypeConfiguration<Paciente>
{
    public void Configure(EntityTypeBuilder<Paciente> builder)
    {
        builder.ToTable("Pacientes", "Personas");
        builder.ConfigureAuditableEntity();

        builder.Property(x => x.CodigoPaciente)
            .HasMaxLength(30)
            .IsRequired();

        builder.HasIndex(x => x.CodigoPaciente)
            .IsUnique();

        builder.Property(x => x.OcupacionProfesion)
            .HasMaxLength(150);

        builder.HasOne(x => x.Persona)
            .WithMany()
            .HasForeignKey(x => x.PersonaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
