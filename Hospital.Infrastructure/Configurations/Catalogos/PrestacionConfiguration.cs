using Hospital.Domain.Entities.Catalogos;
using Hospital.Infrastructure.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Configurations.Catalogos;

public class PrestacionConfiguration : IEntityTypeConfiguration<Prestacion>
{
    public void Configure(EntityTypeBuilder<Prestacion> builder)
    {
        builder.ToTable("Prestaciones", "Catalogos");
        builder.ConfigureAuditableEntity();

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Codigo)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Nombre)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.Descripcion)
            .HasMaxLength(300);

        builder.Property(x => x.Precio)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.HasIndex(x => x.Codigo)
            .IsUnique();

        builder.HasOne(x => x.Servicio)
            .WithMany(x => x.Prestaciones)
            .HasForeignKey(x => x.ServicioId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}