using Hospital.Domain.Entities;
using Hospital.Domain.Entities.Personas;
using Hospital.Infrastructure.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Configurations;

public class AtencionConfiguration : IEntityTypeConfiguration<Atencion>
{
    public void Configure(EntityTypeBuilder<Atencion> builder)
    {
        builder.ToTable("Atenciones");
        builder.ConfigureAuditableEntity();
        
        builder.Property(x => x.PacienteId)
            .IsRequired();

        builder.Property(x => x.TipoAtencionId)
            .IsRequired();

        builder.Property(x => x.FormularioId)
            .IsRequired();

        builder.Property(x => x.Fecha)
            .HasColumnType("date")
            .IsRequired();

        builder.Property(x => x.Hora)
            .IsRequired();

        builder.Property(x => x.Estado)
            .HasMaxLength(30)
            .HasDefaultValue("Abierta")
            .IsRequired();

        builder.Property(x => x.MotivoConsulta)
            .HasMaxLength(500);

        builder.HasOne(x => x.Paciente)
            .WithMany()
            .HasForeignKey(x => x.PacienteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.TipoAtencion)
            .WithMany(x => x.Atenciones)
            .HasForeignKey(x => x.TipoAtencionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Formulario)
            .WithMany(x => x.Atenciones)
            .HasForeignKey(x => x.FormularioId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
