using Hospital.Application.Common;
using Hospital.Application.DTOs.Catalogo;
using Hospital.Application.Exceptions;
using Hospital.Application.Interfaces;
using Hospital.Infrastructure.Data;
using Hospital.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Services;

public class CatalogoGrupoService(AppDbContext context) : ICatalogoGrupoService
{
    private readonly CatalogoGrupoMapper _mapper = new();

    public async Task<int> CreateAsync(CreateCatalogoGrupoDto dto)
    {
        var exists = await context.CatalogoGrupos
            .AnyAsync(x => x.Codigo == dto.Codigo);

        if (exists)
            throw new ConflictException($"Ya existe un registro con el código '{dto.Codigo}'.");

        var catalogoGrupo = _mapper.ToEntity(dto);
        context.CatalogoGrupos.Add(catalogoGrupo);
        await context.SaveChangesAsync();

        return catalogoGrupo.Id;
    }

    public async Task<PagedResult<CatalogoGrupoResponseDto>> GetPagedAsync(PagedQuery query)
    {
        var q = context.CatalogoGrupos
            .AsNoTracking()
            .Where(x => x.Activo);

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            var search = query.Search.Trim();
            q = q.Where(x => x.Codigo.Contains(search) || x.Nombre.Contains(search));
        }

        q = q.OrderBy(x => x.Nombre);

        var totalCount = await q.CountAsync();
        var entities = await q
            .Skip(query.Skip)
            .Take(query.NormalizedPageSize)
            .ToListAsync();

        var items = entities.Select(_mapper.ToDto).ToList();
        return PagedResult<CatalogoGrupoResponseDto>.Create(items, totalCount, query);
    }

    public async Task<CatalogoGrupoResponseDto?> GetByIdAsync(int id)
    {
        var catalogoGrupo = await context.CatalogoGrupos
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        return catalogoGrupo is null ? null : _mapper.ToDto(catalogoGrupo);
    }

    public async Task UpdateAsync(int id, UpdateCatalogoGrupoDto dto)
    {
        var entity = await context.CatalogoGrupos
            .FirstOrDefaultAsync(x => x.Id == id);

        if (entity is null)
            throw new NotFoundException("Catálogo no encontrado.");

        _mapper.UpdateEntity(dto, entity);

        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await context.CatalogoGrupos
            .FirstOrDefaultAsync(x => x.Id == id);

        if (entity is null)
            throw new NotFoundException("Catálogo no encontrado.");

        entity.Activo = false;
        await context.SaveChangesAsync();
    }

    public async Task<List<CatalogoGrupoItemResponseDto>>
        GroupByCatalogoItemAsync()
    {
        return await context.CatalogoGrupos
            .AsNoTracking()
            .Where(x => x.Activo)
            .OrderBy(x => x.Nombre)
            .Select(x => new CatalogoGrupoItemResponseDto(
                x.Id,
                x.Codigo,
                x.Nombre,
                x.Items
                    .Where(i => i.Activo)
                    .OrderBy(i => i.Orden)
                    .Select(i => new ItemDto(
                        i.Id,
                        i.Codigo,
                        i.Nombre,
                        i.Valor,
                        i.Orden,
                        i.Activo
                    ))
                    .ToList()
            ))
            .ToListAsync();
    }
}