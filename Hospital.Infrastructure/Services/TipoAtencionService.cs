using Hospital.Application.Common;
using Hospital.Application.DTOs.TiposAtencion;
using Hospital.Application.Exceptions;
using Hospital.Application.Interfaces;
using Hospital.Domain.Entities;
using Hospital.Infrastructure.Data;
using Hospital.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Services;

public class TipoAtencionService(AppDbContext context) : ITipoAtencionService
{
    private readonly TipoAtencionMapper _mapper = new();

    public async Task<int> CreateAsync(CreateTipoAtencionDto dto)
    {
        var exists = await context.TiposAtencion
            .AnyAsync(x => x.Codigo == dto.Codigo);

        if (exists)
            throw new ConflictException($"Ya existe un tipo de atención con el código '{dto.Codigo}'.");

        var entity = _mapper.ToEntity(dto);
        context.TiposAtencion.Add(entity);
        await context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<PagedResult<TipoAtencionResponseDto>> GetPagedAsync(PagedQuery query)
    {
        var q = context.TiposAtencion
            .AsNoTracking()
            .Where(x => x.Activo);

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            var search = query.Search.Trim();
            q = q.Where(x => x.Nombre.Contains(search) || x.Codigo.Contains(search));
        }

        q = q.OrderBy(x => x.Orden);

        var totalCount = await q.CountAsync();
        var entities = await q
            .Skip(query.Skip)
            .Take(query.NormalizedPageSize)
            .ToListAsync();

        var items = entities.Select(_mapper.ToDto).ToList();
        return PagedResult<TipoAtencionResponseDto>.Create(items, totalCount, query);
    }

    public async Task<TipoAtencionResponseDto?> GetByIdAsync(int id)
    {
        var entity = await context.TiposAtencion
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id && x.Activo);

        return entity is null ? null : _mapper.ToDto(entity);
    }

    public async Task UpdateAsync(int id, UpdateTipoAtencionDto dto)
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

    private async Task<TipoAtencion> GetEntityByIdAsync(int id)
    {
        var entity = await context.TiposAtencion
            .FirstOrDefaultAsync(x => x.Id == id);

        return entity ?? throw new NotFoundException("Tipo de atención no encontrado.");
    }
}
