using Hospital.Application.Common;
using Hospital.Application.DTOs.Formularios;
using Hospital.Application.Exceptions;
using Hospital.Application.Interfaces;
using Hospital.Domain.Entities;
using Hospital.Infrastructure.Data;
using Hospital.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Services;

public class FormularioService(AppDbContext context) : IFormularioService
{
    private readonly FormularioMapper _mapper = new();

    public async Task<int> CreateAsync(CreateFormularioDto dto)
    {
        await EnsureTipoAtencionExistsAsync(dto.TipoAtencionId);

        var entity = _mapper.ToEntity(dto);
        context.Formularios.Add(entity);
        await context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<PagedResult<FormularioResponseDto>> GetPagedAsync(PagedQuery query)
    {
        var q = context.Formularios
            .AsNoTracking()
            .Where(x => x.Activo);

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            var search = query.Search.Trim();
            q = q.Where(x => x.Nombre.Contains(search));
        }

        q = q.OrderBy(x => x.Nombre);

        var totalCount = await q.CountAsync();
        var entities = await q
            .Skip(query.Skip)
            .Take(query.NormalizedPageSize)
            .ToListAsync();

        var items = entities.Select(_mapper.ToDto).ToList();
        return PagedResult<FormularioResponseDto>.Create(items, totalCount, query);
    }

    public async Task<FormularioResponseDto?> GetByIdAsync(int id)
    {
        var entity = await context.Formularios
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id && x.Activo);

        return entity is null ? null : _mapper.ToDto(entity);
    }

    public async Task UpdateAsync(int id, UpdateFormularioDto dto)
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

    public async Task<List<FormularioResponseDto>> GetByTipoAtencionIdAsync(int tipoAtencionId)
    {
        return await context.Formularios
            .AsNoTracking()
            .Where(x => x.Activo && x.TipoAtencionId == tipoAtencionId)
            .Select(x => _mapper.ToDto(x))
            .ToListAsync();
    }

    private async Task<Formulario> GetEntityByIdAsync(int id)
    {
        var entity = await context.Formularios
            .FirstOrDefaultAsync(x => x.Id == id);

        return entity ?? throw new NotFoundException("Formulario no encontrado.");
    }

    private async Task EnsureTipoAtencionExistsAsync(int tipoAtencionId)
    {
        var exists = await context.TiposAtencion
            .AnyAsync(x => x.Id == tipoAtencionId && x.Activo);

        if (!exists)
            throw new NotFoundException("El tipo de atención no existe.");
    }
}
