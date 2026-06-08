using Hospital.Application.Common;
using Hospital.Application.DTOs.Catalogos;
using Hospital.Application.Exceptions;
using Hospital.Application.Interfaces;
using Hospital.Infrastructure.Data;
using Hospital.Infrastructure.Mappings.Catalogos;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Services.Catalogos;

public class EspecialidadService(AppDbContext context) : IEspecialidadService
{
    private readonly EspecialidadMapper _mapper = new();

    public async Task<int> CreateAsync(CreateEspecialidadDto dto)
    {
        await ValidateCodigoAsync(dto.Codigo);

        var entity = _mapper.ToEntity(dto);
        context.Especialidades.Add(entity);
        await context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<PagedResult<EspecialidadResponseDto>> GetPagedAsync(PagedQuery query)
    {
        var q = context.Especialidades
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
        return PagedResult<EspecialidadResponseDto>.Create(items, totalCount, query);
    }

    public async Task<EspecialidadResponseDto?> GetByIdAsync(int id)
    {
        var entity = await context.Especialidades
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id && x.Activo);

        return entity is null ? null : _mapper.ToDto(entity);
    }

    public async Task UpdateAsync(int id, UpdateEspecialidadDto dto)
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

    private async Task<Domain.Entities.Catalogos.Especialidad> GetEntityByIdAsync(int id)
    {
        var entity = await context.Especialidades
            .FirstOrDefaultAsync(x => x.Id == id);

        return entity ?? throw new NotFoundException("Especialidad no encontrada.");
    }

    private async Task ValidateCodigoAsync(string codigo, int? excludeId = null)
    {
        var exists = await context.Especialidades.AnyAsync(x =>
            x.Codigo == codigo &&
            (!excludeId.HasValue || x.Id != excludeId.Value));

        if (exists)
            throw new ConflictException($"Ya existe una especialidad con el código '{codigo}'.");
    }
}

public class ProfesionService(AppDbContext context) : IProfesionService
{
    private readonly ProfesionMapper _mapper = new();

    public async Task<int> CreateAsync(CreateProfesionDto dto)
    {
        await ValidateCodigoAsync(dto.Codigo);

        var entity = _mapper.ToEntity(dto);
        context.Profesiones.Add(entity);
        await context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<PagedResult<ProfesionResponseDto>> GetPagedAsync(PagedQuery query)
    {
        var q = context.Profesiones
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
        return PagedResult<ProfesionResponseDto>.Create(items, totalCount, query);
    }

    public async Task<ProfesionResponseDto?> GetByIdAsync(int id)
    {
        var entity = await context.Profesiones
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id && x.Activo);

        return entity is null ? null : _mapper.ToDto(entity);
    }

    public async Task UpdateAsync(int id, UpdateProfesionDto dto)
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

    private async Task<Domain.Entities.Catalogos.Profesion> GetEntityByIdAsync(int id)
    {
        var entity = await context.Profesiones
            .FirstOrDefaultAsync(x => x.Id == id);

        return entity ?? throw new NotFoundException("Profesión no encontrada.");
    }

    private async Task ValidateCodigoAsync(string codigo, int? excludeId = null)
    {
        var exists = await context.Profesiones.AnyAsync(x =>
            x.Codigo == codigo &&
            (!excludeId.HasValue || x.Id != excludeId.Value));

        if (exists)
            throw new ConflictException($"Ya existe una profesión con el código '{codigo}'.");
    }
}

public class CargoService(AppDbContext context) : ICargoService
{
    private readonly CargoMapper _mapper = new();

    public async Task<int> CreateAsync(CreateCargoDto dto)
    {
        await ValidateCodigoAsync(dto.Codigo);

        var entity = _mapper.ToEntity(dto);
        context.Cargos.Add(entity);
        await context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<PagedResult<CargoResponseDto>> GetPagedAsync(PagedQuery query)
    {
        var q = context.Cargos
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
        return PagedResult<CargoResponseDto>.Create(items, totalCount, query);
    }

    public async Task<CargoResponseDto?> GetByIdAsync(int id)
    {
        var entity = await context.Cargos
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id && x.Activo);

        return entity is null ? null : _mapper.ToDto(entity);
    }

    public async Task UpdateAsync(int id, UpdateCargoDto dto)
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

    private async Task<Domain.Entities.Catalogos.Cargo> GetEntityByIdAsync(int id)
    {
        var entity = await context.Cargos
            .FirstOrDefaultAsync(x => x.Id == id);

        return entity ?? throw new NotFoundException("Cargo no encontrado.");
    }

    private async Task ValidateCodigoAsync(string codigo, int? excludeId = null)
    {
        var exists = await context.Cargos.AnyAsync(x =>
            x.Codigo == codigo &&
            (!excludeId.HasValue || x.Id != excludeId.Value));

        if (exists)
            throw new ConflictException($"Ya existe un cargo con el código '{codigo}'.");
    }
}

public class TipoAtencionCatalogoService(AppDbContext context) : ITipoAtencionCatalogoService
{
    private readonly TipoAtencionCatalogoMapper _mapper = new();

    public async Task<int> CreateAsync(CreateTipoAtencionCatalogoDto dto)
    {
        await ValidateCodigoAsync(dto.Codigo);

        var entity = _mapper.ToEntity(dto);
        context.CatalogoTiposAtencion.Add(entity);
        await context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<PagedResult<TipoAtencionCatalogoResponseDto>> GetPagedAsync(PagedQuery query)
    {
        var q = context.CatalogoTiposAtencion
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
        return PagedResult<TipoAtencionCatalogoResponseDto>.Create(items, totalCount, query);
    }

    public async Task<TipoAtencionCatalogoResponseDto?> GetByIdAsync(int id)
    {
        var entity = await context.CatalogoTiposAtencion
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id && x.Activo);

        return entity is null ? null : _mapper.ToDto(entity);
    }

    public async Task UpdateAsync(int id, UpdateTipoAtencionCatalogoDto dto)
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

    private async Task<Domain.Entities.Catalogos.TipoAtencion> GetEntityByIdAsync(int id)
    {
        var entity = await context.CatalogoTiposAtencion
            .FirstOrDefaultAsync(x => x.Id == id);

        return entity ?? throw new NotFoundException("Tipo de atención no encontrado.");
    }

    private async Task ValidateCodigoAsync(string codigo, int? excludeId = null)
    {
        var exists = await context.CatalogoTiposAtencion.AnyAsync(x =>
            x.Codigo == codigo &&
            (!excludeId.HasValue || x.Id != excludeId.Value));

        if (exists)
            throw new ConflictException($"Ya existe un tipo de atención con el código '{codigo}'.");
    }
}
