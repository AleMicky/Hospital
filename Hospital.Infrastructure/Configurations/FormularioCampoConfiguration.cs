using Hospital.Domain.Entities;
using Hospital.Infrastructure.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Configurations;

public class FormularioCampoConfiguration : IEntityTypeConfiguration<FormularioCampo>
{
    public void Configure(EntityTypeBuilder<FormularioCampo> builder)
    {
        builder.ToTable("FormularioCampos");
        builder.ConfigureAuditableEntity();
        
        builder.Property(x => x.FormularioId)
            .IsRequired();

        builder.Property(x => x.NombreCampo)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Etiqueta)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.Seccion)
            .HasMaxLength(100)
            .HasDefaultValue(string.Empty);

        builder.Property(x => x.TipoDato)
            .IsRequired();

        builder.Property(x => x.TipoControl)
            .IsRequired();

        builder.Property(x => x.Placeholder)
            .HasMaxLength(200);

        builder.Property(x => x.ValorDefault)
            .HasMaxLength(500);

        builder.Property(x => x.OpcionesJson)
            .HasMaxLength(2000);

        builder.Property(x => x.Orden)
            .HasDefaultValue(0);

        builder.Property(x => x.Minimo)
            .HasPrecision(18, 4);

        builder.Property(x => x.Maximo)
            .HasPrecision(18, 4);

        builder.HasIndex(x => new { x.FormularioId, x.NombreCampo })
            .IsUnique();

        builder.HasOne(x => x.Formulario)
            .WithMany(x => x.Campos)
            .HasForeignKey(x => x.FormularioId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
