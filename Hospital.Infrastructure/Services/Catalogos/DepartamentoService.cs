using Hospital.Application.Common;
using Hospital.Application.DTOs.Catalogos;
using Hospital.Application.Exceptions;
using Hospital.Application.Interfaces;
using Hospital.Domain.Entities.Catalogos;
using Hospital.Infrastructure.Data;
using Hospital.Infrastructure.Mappings.Catalogos;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Services.Catalogos;

public class DepartamentoService(AppDbContext context) : IDepartamentoService
{
    private readonly DepartamentoMapper _mapper = new();

    public async Task<int> CreateAsync(CreateDepartamentoDto dto)
    {
        await EnsureAreaExistsAsync(dto.AreaId);
        await ValidateCodigoAsync(dto.Codigo);

        var entity = _mapper.ToEntity(dto);
        context.Departamentos.Add(entity);
        await context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<PagedResult<DepartamentoResponseDto>> GetPagedAsync(PagedQuery query)
    {
        var q = context.Departamentos
            .AsNoTracking()
            .Include(x => x.Area)
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
        return PagedResult<DepartamentoResponseDto>.Create(items, totalCount, query);
    }

    public async Task<DepartamentoResponseDto?> GetByIdAsync(int id)
    {
        var entity = await context.Departamentos
            .AsNoTracking()
            .Include(x => x.Area)
            .FirstOrDefaultAsync(x => x.Id == id && x.Activo);

        return entity is null ? null : _mapper.ToDto(entity);
    }

    public async Task UpdateAsync(int id, UpdateDepartamentoDto dto)
    {
        var entity = await GetEntityByIdAsync(id);
        await EnsureAreaExistsAsync(dto.AreaId);
        await ValidateCodigoAsync(dto.Codigo, id);
        _mapper.UpdateEntity(dto, entity);
        entity.AreaId = dto.AreaId;
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetEntityByIdAsync(id);
        entity.Activo = false;
        await context.SaveChangesAsync();
    }

    public async Task<List<ServicioResponseDto>> GetServiciosByDepartamentoIdAsync(int departamentoId)
    {
        await EnsureDepartamentoExistsAsync(departamentoId);

        var servicioMapper = new ServicioMapper();

        return await context.Servicios
            .AsNoTracking()
            .Include(x => x.Departamento)
            .Where(x => x.Activo && x.DepartamentoId == departamentoId)
            .OrderBy(x => x.Nombre)
            .Select(x => servicioMapper.ToDto(x))
            .ToListAsync();
    }

    private async Task<Departamento> GetEntityByIdAsync(int id)
    {
        var entity = await context.Departamentos
            .FirstOrDefaultAsync(x => x.Id == id);

        return entity ?? throw new NotFoundException("Departamento no encontrado.");
    }

    private async Task EnsureAreaExistsAsync(int areaId)
    {
        var exists = await context.Areas
            .AnyAsync(x => x.Id == areaId && x.Activo);

        if (!exists)
            throw new NotFoundException("Área no encontrada.");
    }

    private async Task EnsureDepartamentoExistsAsync(int id)
    {
        var exists = await context.Departamentos
            .AnyAsync(x => x.Id == id && x.Activo);

        if (!exists)
            throw new NotFoundException("Departamento no encontrado.");
    }

    private async Task ValidateCodigoAsync(string codigo, int? excludeId = null)
    {
        var exists = await context.Departamentos.AnyAsync(x =>
            x.Codigo == codigo &&
            (!excludeId.HasValue || x.Id != excludeId.Value));

        if (exists)
            throw new ConflictException($"Ya existe un departamento con el código '{codigo}'.");
    }
}
