using Hospital.Application.Common;
using Hospital.Application.DTOs.Catalogo;
using Hospital.Application.Exceptions;
using Hospital.Application.Interfaces;
using Hospital.Domain.Entities;
using Hospital.Infrastructure.Data;
using Hospital.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Services;

public class CatalogoItemService(AppDbContext context) : ICatalogoItemService
{
    private readonly CatalogoItemMapper _mapper = new();

    public async Task<int> CreateAsync(CreateCatalogoItemDto dto)
    {
        var existsGrupo = await context.CatalogoGrupos
            .AnyAsync(x => x.Id == dto.CatalogoGrupoId);

        if (!existsGrupo)
            throw new NotFoundException(
                "El grupo de catálogo no existe."
            );

        await ValidateCodigoAsync(dto.Codigo, dto.CatalogoGrupoId);

        var entity = _mapper.ToEntity(dto);

        context.CatalogoItems.Add(entity);
        await context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<PagedResult<CatalogoItemResponseDto>> GetPagedAsync(PagedQuery query)
    {
        var q = context.CatalogoItems
            .AsNoTracking()
            .Where(x => x.Activo);

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            var search = query.Search.Trim();
            q = q.Where(x => x.Codigo.Contains(search) || x.Nombre.Contains(search));
        }

        q = q.OrderBy(x => x.Orden);

        var totalCount = await q.CountAsync();
        var entities = await q
            .Skip(query.Skip)
            .Take(query.NormalizedPageSize)
            .ToListAsync();

        var items = entities.Select(_mapper.ToDto).ToList();
        return PagedResult<CatalogoItemResponseDto>.Create(items, totalCount, query);
    }

    public async Task<CatalogoItemResponseDto?> GetByIdAsync(int id)
    {
        var entity = await context.CatalogoItems
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id && x.Activo);

        return entity is null ? null : _mapper.ToDto(entity);
    }

    public async Task UpdateAsync(int id, UpdateCatalogoItemDto dto)
    {
        var entity = await GetEntityByIdAsync(id);

        await ValidateCodigoAsync(dto.Codigo, dto.CatalogoGrupoId, id);

        _mapper.UpdateEntity(dto, entity);

        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetEntityByIdAsync(id);
        entity.Activo = false;

        await context.SaveChangesAsync();
    }

    public async Task<List<CatalogoItemResponseDto>> GetByCatalogoGrupoIdAsync(int id)
    {
        return await context.CatalogoItems
            .AsNoTracking()
            .Where(x => x.Activo && x.CatalogoGrupoId == id)
            .Select(x => _mapper.ToDto(x))
            .ToListAsync();
    }

    private async Task<CatalogoItem> GetEntityByIdAsync(int id)
    {
        var entity = await context.CatalogoItems
            .FirstOrDefaultAsync(x => x.Id == id);

        return entity ?? throw new NotFoundException("Catálogo item no encontrado.");
    }

    private async Task ValidateCodigoAsync(
        string codigo,
        int catalogoGrupoId,
        int? excludeId = null)
    {
        var exists = await context.CatalogoItems.AnyAsync(x =>
            x.Codigo == codigo &&
            x.CatalogoGrupoId == catalogoGrupoId &&
            (!excludeId.HasValue || x.Id != excludeId.Value));

        if (exists)
            throw new ConflictException(
                $"Ya existe un catálogo item con el código '{codigo}'.");
    }
}