using Hospital.Domain.Entities;
using Hospital.Infrastructure.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Configurations;

public class AtencionValorConfiguration : IEntityTypeConfiguration<AtencionValor>
{
    public void Configure(EntityTypeBuilder<AtencionValor> builder)
    {
        builder.ToTable("AtencionValores");
        builder.ConfigureAuditableEntity();
        
        builder.Property(x => x.AtencionId)
            .IsRequired();

        builder.Property(x => x.FormularioCampoId)
            .IsRequired();

        builder.Property(x => x.ValorTexto)
            .HasMaxLength(2000);

        builder.Property(x => x.ValorDecimal)
            .HasPrecision(18, 4);

        builder.Property(x => x.ValorFecha)
            .HasColumnType("date");

        builder.HasIndex(x => new { x.AtencionId, x.FormularioCampoId })
            .IsUnique();

        builder.HasOne(x => x.Atencion)
            .WithMany(x => x.Valores)
            .HasForeignKey(x => x.AtencionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.FormularioCampo)
            .WithMany(x => x.Valores)
            .HasForeignKey(x => x.FormularioCampoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
