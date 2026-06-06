using Hospital.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Infrastructure.Configurations.Common;

public static class AuditableConfiguration
{
    public static void ConfigureAuditableEntity<T>(this EntityTypeBuilder<T> builder)
        where T : AuditableEntity
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.CreatedBy)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.Property(x => x.UpdatedBy)
            .HasMaxLength(100);

        builder.Property(x => x.Activo)
            .IsRequired()
            .HasDefaultValue(true);
    }
}