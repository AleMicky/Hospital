using Hospital.Domain.Entities;
using Hospital.Infrastructure.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Configurations;

public class FormularioConfiguration : IEntityTypeConfiguration<Formulario>
{
    public void Configure(EntityTypeBuilder<Formulario> builder)
    {
        builder.ToTable("Formularios");
        builder.ConfigureAuditableEntity();
        
        builder.Property(x => x.TipoAtencionId)
            .IsRequired();

        builder.Property(x => x.Nombre)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.Descripcion)
            .HasMaxLength(500);

        builder.Property(x => x.Version)
            .HasDefaultValue(1);

        builder.Property(x => x.EsPlantilla)
            .HasDefaultValue(true);

        builder.HasOne(x => x.TipoAtencion)
            .WithMany(x => x.Formularios)
            .HasForeignKey(x => x.TipoAtencionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
