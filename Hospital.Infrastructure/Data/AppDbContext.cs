using Hospital.Application.Interfaces;
using Hospital.Domain.Common;
using Hospital.Domain.Entities;
using Hospital.Domain.Entities.Personas;
using Hospital.Domain.Entities.Catalogos;
using Hospital.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TipoAtencionCatalogo = Hospital.Domain.Entities.Catalogos.TipoAtencion;
using TipoAtencionClinico = Hospital.Domain.Entities.TipoAtencion;

namespace Hospital.Infrastructure.Data;

public class AppDbContext(
    DbContextOptions<AppDbContext> options,
    ICurrentUserService currentUserService)
    : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>(options)
{
    private readonly ICurrentUserService _currentUserService = currentUserService;
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyAudit();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        ApplyAudit();
        return base.SaveChanges();
    }
    
    
    public DbSet<Persona> Personas => Set<Persona>();
    public DbSet<Paciente> Pacientes => Set<Paciente>();
    public DbSet<CatalogoGrupo> CatalogoGrupos => Set<CatalogoGrupo>();
    public DbSet<CatalogoItem> CatalogoItems => Set<CatalogoItem>();
    public DbSet<TipoAtencionClinico> TiposAtencion => Set<TipoAtencionClinico>();
    public DbSet<Formulario> Formularios => Set<Formulario>();
    public DbSet<FormularioCampo> FormularioCampos => Set<FormularioCampo>();
    public DbSet<Atencion> Atenciones => Set<Atencion>();
    public DbSet<AtencionValor> AtencionValores => Set<AtencionValor>();
    
    // Catalogos
    public DbSet<Area> Areas => Set<Area>();
    public DbSet<Departamento> Departamentos => Set<Departamento>();
    public DbSet<Servicio> Servicios => Set<Servicio>();
    public DbSet<Prestacion> Prestaciones => Set<Prestacion>();
    public DbSet<TipoAtencionCatalogo> CatalogoTiposAtencion => Set<TipoAtencionCatalogo>();
    public DbSet<Especialidad> Especialidades => Set<Especialidad>();
    public DbSet<Profesion> Profesiones => Set<Profesion>();
    public DbSet<Cargo> Cargos => Set<Cargo>();
    
    private void ApplyAudit()
    {
        var entries = ChangeTracker
            .Entries<AuditableEntity>()
            .Where(x => x.State is EntityState.Added or EntityState.Modified)
            .ToList();

        if (entries.Count == 0)
            return;

        var auditUser = _currentUserService.AuditUserName;
        var now = DateTime.UtcNow;

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = now;
                entry.Entity.CreatedBy = auditUser;
                entry.Entity.Activo = true;
                continue;
            }

            entry.Entity.UpdatedAt = now;
            entry.Entity.UpdatedBy = auditUser;
            entry.Property(x => x.CreatedAt).IsModified = false;
            entry.Property(x => x.CreatedBy).IsModified = false;
        }
    }
}