using Hospital.Domain.Entities.Catalogos;
using Hospital.Infrastructure.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Configurations.Catalogos;

public class ServicioConfiguration : IEntityTypeConfiguration<Servicio>
{
    public void Configure(EntityTypeBuilder<Servicio> builder)
    {
        builder.ToTable("Servicios", "Catalogos");
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

        builder.HasIndex(x => x.Codigo)
            .IsUnique();

        builder.HasOne(x => x.Departamento)
            .WithMany(x => x.Servicios)
            .HasForeignKey(x => x.DepartamentoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}