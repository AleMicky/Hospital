using Hospital.Domain.Entities;
using Hospital.Infrastructure.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Configurations;

public class CatalogoItemConfiguration : IEntityTypeConfiguration<CatalogoItem>
{
    public void Configure(EntityTypeBuilder<CatalogoItem> builder)
    {
        builder.ToTable("CatalogoItems");

        builder.ConfigureAuditableEntity();
        
        builder.Property(x => x.CatalogoGrupoId)
            .IsRequired();

        builder.Property(x => x.Codigo)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Nombre)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Valor)
            .HasMaxLength(100);

        builder.Property(x => x.Orden)
            .HasDefaultValue(0);

        builder.HasIndex(x => new
        {
            x.CatalogoGrupoId,
            x.Codigo
        }).IsUnique();

        builder.HasOne(x => x.CatalogoGrupo)
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.CatalogoGrupoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}