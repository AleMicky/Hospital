using Hospital.Domain.Entities;
using Hospital.Domain.Entities.Personas;
using Hospital.Infrastructure.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Configurations.Personas;

public class PersonaConfiguration : IEntityTypeConfiguration<Persona>
{
    public void Configure(EntityTypeBuilder<Persona> builder)
    {
        builder.ToTable("Personas", "Personas");
        builder.ConfigureAuditableEntity();

        builder.Property(x => x.Nombres)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.ApellidoPaterno)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.ApellidoMaterno)
            .HasMaxLength(100);

        builder.Property(x => x.NumeroDocumento)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.ComplementoDocumento)
            .HasMaxLength(10);

        builder.Property(x => x.FechaNacimiento)
            .HasColumnType("date")
            .IsRequired();

        builder.Property(x => x.Telefono)
            .HasMaxLength(30);

        builder.Property(x => x.Direccion)
            .HasMaxLength(250);

        builder.HasOne(x => x.TipoDocumento)
            .WithMany()
            .HasForeignKey(x => x.TipoDocumentoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ExtensionDocumento)
            .WithMany()
            .HasForeignKey(x => x.ExtensionDocumentoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Sexo)
            .WithMany()
            .HasForeignKey(x => x.SexoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.EstadoCivil)
            .WithMany()
            .HasForeignKey(x => x.EstadoCivilId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new
        {
            x.TipoDocumentoId,
            x.NumeroDocumento,
            x.ComplementoDocumento,
            x.ExtensionDocumentoId
        }).IsUnique();
    }
}
