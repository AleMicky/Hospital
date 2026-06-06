using Hospital.Domain.Entities;
using Hospital.Infrastructure.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Configurations;

public class CatalogoGrupoConfiguration : IEntityTypeConfiguration<CatalogoGrupo>
{
    public void Configure(EntityTypeBuilder<CatalogoGrupo> builder)
    {
        builder.ToTable("CatalogoGrupos");
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
    }
}