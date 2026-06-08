using Hospital.Application.Common;
using Hospital.Application.DTOs.Catalogos;
using Hospital.Application.Exceptions;
using Hospital.Application.Interfaces;
using Hospital.Domain.Entities.Catalogos;
using Hospital.Infrastructure.Data;
using Hospital.Infrastructure.Mappings.Catalogos;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Services.Catalogos;

public class PrestacionService(AppDbContext context) : IPrestacionService
{
    private readonly PrestacionMapper _mapper = new();

    public async Task<int> CreateAsync(CreatePrestacionDto dto)
    {
        await EnsureServicioExistsAsync(dto.ServicioId);
        await ValidateCodigoAsync(dto.Codigo);

        var entity = _mapper.ToEntity(dto);
        context.Prestaciones.Add(entity);
        await context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<PagedResult<PrestacionResponseDto>> GetPagedAsync(PagedQuery query)
    {
        var q = context.Prestaciones
            .AsNoTracking()
            .Include(x => x.Servicio)
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
        return PagedResult<PrestacionResponseDto>.Create(items, totalCount, query);
    }

    public async Task<PrestacionResponseDto?> GetByIdAsync(int id)
    {
        var entity = await context.Prestaciones
            .AsNoTracking()
            .Include(x => x.Servicio)
            .FirstOrDefaultAsync(x => x.Id == id && x.Activo);

        return entity is null ? null : _mapper.ToDto(entity);
    }

    public async Task UpdateAsync(int id, UpdatePrestacionDto dto)
    {
        var entity = await GetEntityByIdAsync(id);
        await EnsureServicioExistsAsync(dto.ServicioId);
        await ValidateCodigoAsync(dto.Codigo, id);
        _mapper.UpdateEntity(dto, entity);
        entity.ServicioId = dto.ServicioId;
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetEntityByIdAsync(id);
        entity.Activo = false;
        await context.SaveChangesAsync();
    }

    private async Task<Prestacion> GetEntityByIdAsync(int id)
    {
        var entity = await context.Prestaciones
            .FirstOrDefaultAsync(x => x.Id == id);

        return entity ?? throw new NotFoundException("Prestación no encontrada.");
    }

    private async Task EnsureServicioExistsAsync(int servicioId)
    {
        var exists = await context.Servicios
            .AnyAsync(x => x.Id == servicioId && x.Activo);

        if (!exists)
            throw new NotFoundException("Servicio no encontrado.");
    }

    private async Task ValidateCodigoAsync(string codigo, int? excludeId = null)
    {
        var exists = await context.Prestaciones.AnyAsync(x =>
            x.Codigo == codigo &&
            (!excludeId.HasValue || x.Id != excludeId.Value));

        if (exists)
            throw new ConflictException($"Ya existe una prestación con el código '{codigo}'.");
    }
}
