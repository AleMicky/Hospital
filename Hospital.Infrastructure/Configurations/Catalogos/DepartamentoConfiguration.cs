using Hospital.Domain.Entities.Catalogos;
using Hospital.Infrastructure.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Configurations.Catalogos;

public class DepartamentoConfiguration : IEntityTypeConfiguration<Departamento>
{
    public void Configure(EntityTypeBuilder<Departamento> builder)
    {
        builder.ToTable("Departamentos", "Catalogos");
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

        builder.HasOne(x => x.Area)
            .WithMany(x => x.Departamentos)
            .HasForeignKey(x => x.AreaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.AreaId);
    }
}