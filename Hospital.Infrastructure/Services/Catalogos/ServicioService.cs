using Hospital.Application.Common;
using Hospital.Application.DTOs.Catalogos;
using Hospital.Application.Exceptions;
using Hospital.Application.Interfaces;
using Hospital.Domain.Entities.Catalogos;
using Hospital.Infrastructure.Data;
using Hospital.Infrastructure.Mappings.Catalogos;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Services.Catalogos;

public class ServicioService(AppDbContext context) : IServicioService
{
    private readonly ServicioMapper _mapper = new();

    public async Task<int> CreateAsync(CreateServicioDto dto)
    {
        await EnsureDepartamentoExistsAsync(dto.DepartamentoId);
        await ValidateCodigoAsync(dto.Codigo);

        var entity = _mapper.ToEntity(dto);
        context.Servicios.Add(entity);
        await context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<PagedResult<ServicioResponseDto>> GetPagedAsync(PagedQuery query)
    {
        var q = context.Servicios
            .AsNoTracking()
            .Include(x => x.Departamento)
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
        return PagedResult<ServicioResponseDto>.Create(items, totalCount, query);
    }

    public async Task<ServicioResponseDto?> GetByIdAsync(int id)
    {
        var entity = await context.Servicios
            .AsNoTracking()
            .Include(x => x.Departamento)
            .FirstOrDefaultAsync(x => x.Id == id && x.Activo);

        return entity is null ? null : _mapper.ToDto(entity);
    }

    public async Task UpdateAsync(int id, UpdateServicioDto dto)
    {
        var entity = await GetEntityByIdAsync(id);
        await EnsureDepartamentoExistsAsync(dto.DepartamentoId);
        await ValidateCodigoAsync(dto.Codigo, id);
        _mapper.UpdateEntity(dto, entity);
        entity.DepartamentoId = dto.DepartamentoId;
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetEntityByIdAsync(id);
        entity.Activo = false;
        await context.SaveChangesAsync();
    }

    public async Task<List<PrestacionResponseDto>> GetPrestacionesByServicioIdAsync(int servicioId)
    {
        await EnsureServicioExistsAsync(servicioId);

        var prestacionMapper = new PrestacionMapper();

        return await context.Prestaciones
            .AsNoTracking()
            .Include(x => x.Servicio)
            .Where(x => x.Activo && x.ServicioId == servicioId)
            .OrderBy(x => x.Nombre)
            .Select(x => prestacionMapper.ToDto(x))
            .ToListAsync();
    }

    private async Task<Servicio> GetEntityByIdAsync(int id)
    {
        var entity = await context.Servicios
            .FirstOrDefaultAsync(x => x.Id == id);

        return entity ?? throw new NotFoundException("Servicio no encontrado.");
    }

    private async Task EnsureDepartamentoExistsAsync(int departamentoId)
    {
        var exists = await context.Departamentos
            .AnyAsync(x => x.Id == departamentoId && x.Activo);

        if (!exists)
            throw new NotFoundException("Departamento no encontrado.");
    }

    private async Task EnsureServicioExistsAsync(int id)
    {
        var exists = await context.Servicios
            .AnyAsync(x => x.Id == id && x.Activo);

        if (!exists)
            throw new NotFoundException("Servicio no encontrado.");
    }

    private async Task ValidateCodigoAsync(string codigo, int? excludeId = null)
    {
        var exists = await context.Servicios.AnyAsync(x =>
            x.Codigo == codigo &&
            (!excludeId.HasValue || x.Id != excludeId.Value));

        if (exists)
            throw new ConflictException($"Ya existe un servicio con el código '{codigo}'.");
    }
}
