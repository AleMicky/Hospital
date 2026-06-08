using Hospital.Domain.Entities.Catalogos;
using Hospital.Infrastructure.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Configurations.Catalogos;

public class AreaConfiguration : IEntityTypeConfiguration<Area>
{
    public void Configure(EntityTypeBuilder<Area> builder)
    {
        builder.ToTable("Areas", "Catalogos");
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
    }
}
