using Hospital.Domain.Entities;
using Hospital.Infrastructure.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Configurations;

public class TipoAtencionConfiguration : IEntityTypeConfiguration<TipoAtencion>
{
    public void Configure(EntityTypeBuilder<TipoAtencion> builder)
    {
        builder.ToTable("TiposAtencion");
        builder.ConfigureAuditableEntity();
        
        builder.HasIndex(x => x.Codigo)
            .IsUnique();

        builder.Property(x => x.Codigo)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.Nombre)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Descripcion)
            .HasMaxLength(250);

        builder.Property(x => x.Orden)
            .HasDefaultValue(0);

        builder.Property(x => x.Color)
            .HasMaxLength(20);

        builder.Property(x => x.Icono)
            .HasMaxLength(50);
    }
}
