using Hospital.Application.Common;
using Hospital.Application.DTOs.Atenciones;
using Hospital.Application.Exceptions;
using Hospital.Application.Interfaces;
using Hospital.Domain.Entities;
using Hospital.Infrastructure.Data;
using Hospital.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Services;

public class AtencionService(AppDbContext context) : IAtencionService
{
    private readonly AtencionMapper _mapper = new();

    public async Task<int> CreateAsync(CreateAtencionDto dto)
    {
        await EnsurePacienteExistsAsync(dto.PacienteId);
        await EnsureTipoAtencionExistsAsync(dto.TipoAtencionId);
        await EnsureFormularioExistsAsync(dto.FormularioId);

        var entity = _mapper.ToEntity(dto);
        context.Atenciones.Add(entity);
        await context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<PagedResult<AtencionResponseDto>> GetPagedAsync(PagedQuery query)
    {
        var q = context.Atenciones
            .AsNoTracking()
            .Where(x => x.Activo);

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            var search = query.Search.Trim();
            q = q.Where(x => x.Estado.Contains(search) || x.MotivoConsulta.Contains(search));
        }

        q = q.OrderByDescending(x => x.Fecha).ThenByDescending(x => x.Hora);

        var totalCount = await q.CountAsync();
        var entities = await q
            .Skip(query.Skip)
            .Take(query.NormalizedPageSize)
            .ToListAsync();

        var items = entities.Select(_mapper.ToDto).ToList();
        return PagedResult<AtencionResponseDto>.Create(items, totalCount, query);
    }

    public async Task<AtencionResponseDto?> GetByIdAsync(int id)
    {
        var entity = await context.Atenciones
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id && x.Activo);

        return entity is null ? null : _mapper.ToDto(entity);
    }

    public async Task UpdateAsync(int id, UpdateAtencionDto dto)
    {
        var entity = await GetEntityByIdAsync(id);
        _mapper.UpdateEntity(dto, entity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetEntityByIdAsync(id);
        entity.Activo = false;
        await context.SaveChangesAsync();
    }

    public async Task<List<AtencionResponseDto>> GetByPacienteIdAsync(int pacienteId)
    {
        return await context.Atenciones
            .AsNoTracking()
            .Where(x => x.Activo && x.PacienteId == pacienteId)
            .Select(x => _mapper.ToDto(x))
            .ToListAsync();
    }

    private async Task<Atencion> GetEntityByIdAsync(int id)
    {
        var entity = await context.Atenciones
            .FirstOrDefaultAsync(x => x.Id == id);

        return entity ?? throw new NotFoundException("Atención no encontrada.");
    }

    private async Task EnsurePacienteExistsAsync(int pacienteId)
    {
        var exists = await context.Pacientes
            .AnyAsync(x => x.Id == pacienteId && x.Activo);

        if (!exists)
            throw new NotFoundException("El paciente no existe.");
    }

    private async Task EnsureTipoAtencionExistsAsync(int tipoAtencionId)
    {
        var exists = await context.TiposAtencion
            .AnyAsync(x => x.Id == tipoAtencionId && x.Activo);

        if (!exists)
            throw new NotFoundException("El tipo de atención no existe.");
    }

    private async Task EnsureFormularioExistsAsync(int formularioId)
    {
        var exists = await context.Formularios
            .AnyAsync(x => x.Id == formularioId && x.Activo);

        if (!exists)
            throw new NotFoundException("El formulario no existe.");
    }
}
