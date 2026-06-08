using Hospital.Application.Common;
using Hospital.Application.DTOs.Catalogos;
using Hospital.Application.Exceptions;
using Hospital.Application.Interfaces;
using Hospital.Domain.Entities.Catalogos;
using Hospital.Infrastructure.Data;
using Hospital.Infrastructure.Mappings.Catalogos;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Services.Catalogos;

public class AreaService(AppDbContext context) : IAreaService
{
    private readonly AreaMapper _mapper = new();

    public async Task<int> CreateAsync(CreateAreaDto dto)
    {
        await ValidateCodigoAsync(dto.Codigo);

        var entity = _mapper.ToEntity(dto);
        context.Areas.Add(entity);
        await context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<PagedResult<AreaResponseDto>> GetPagedAsync(PagedQuery query)
    {
        var q = context.Areas
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
        return PagedResult<AreaResponseDto>.Create(items, totalCount, query);
    }

    public async Task<AreaResponseDto?> GetByIdAsync(int id)
    {
        var entity = await context.Areas
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id && x.Activo);

        return entity is null ? null : _mapper.ToDto(entity);
    }

    public async Task UpdateAsync(int id, UpdateAreaDto dto)
    {
        var entity = await GetEntityByIdAsync(id);
        await ValidateCodigoAsync(dto.Codigo, id);
        _mapper.UpdateEntity(dto, entity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetEntityByIdAsync(id);
        entity.Activo = false;
        await context.SaveChangesAsync();
    }

    public async Task<List<DepartamentoResponseDto>> GetDepartamentosByAreaIdAsync(int areaId)
    {
        await EnsureAreaExistsAsync(areaId);

        var departamentoMapper = new DepartamentoMapper();

        return await context.Departamentos
            .AsNoTracking()
            .Include(x => x.Area)
            .Where(x => x.Activo && x.AreaId == areaId)
            .OrderBy(x => x.Nombre)
            .Select(x => departamentoMapper.ToDto(x))
            .ToListAsync();
    }

    private async Task<Area> GetEntityByIdAsync(int id)
    {
        var entity = await context.Areas
            .FirstOrDefaultAsync(x => x.Id == id);

        return entity ?? throw new NotFoundException("Área no encontrada.");
    }

    private async Task EnsureAreaExistsAsync(int id)
    {
        var exists = await context.Areas
            .AnyAsync(x => x.Id == id && x.Activo);

        if (!exists)
            throw new NotFoundException("Área no encontrada.");
    }

    private async Task ValidateCodigoAsync(string codigo, int? excludeId = null)
    {
        var exists = await context.Areas.AnyAsync(x =>
            x.Codigo == codigo &&
            (!excludeId.HasValue || x.Id != excludeId.Value));

        if (exists)
            throw new ConflictException($"Ya existe un área con el código '{codigo}'.");
    }
}
