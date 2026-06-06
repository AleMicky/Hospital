using Hospital.Application.Common;
using Hospital.Application.DTOs.Formularios;
using Hospital.Application.Exceptions;
using Hospital.Application.Interfaces;
using Hospital.Domain.Entities;
using Hospital.Infrastructure.Data;
using Hospital.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Services;

public class FormularioCampoService(AppDbContext context) : IFormularioCampoService
{
    private readonly FormularioCampoMapper _mapper = new();

    public async Task<int> CreateAsync(CreateFormularioCampoDto dto)
    {
        await EnsureFormularioExistsAsync(dto.FormularioId);
        await ValidateNombreCampoAsync(dto.NombreCampo, dto.FormularioId);

        var entity = _mapper.ToEntity(dto);
        context.FormularioCampos.Add(entity);
        await context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<PagedResult<FormularioCampoResponseDto>> GetPagedAsync(PagedQuery query)
    {
        var q = context.FormularioCampos
            .AsNoTracking()
            .Where(x => x.Activo);

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            var search = query.Search.Trim();
            q = q.Where(x =>
                x.NombreCampo.Contains(search) ||
                x.Etiqueta.Contains(search) ||
                x.Seccion.Contains(search));
        }

        q = q.OrderBy(x => x.Orden);

        var totalCount = await q.CountAsync();
        var entities = await q
            .Skip(query.Skip)
            .Take(query.NormalizedPageSize)
            .ToListAsync();

        var items = entities.Select(_mapper.ToDto).ToList();
        return PagedResult<FormularioCampoResponseDto>.Create(items, totalCount, query);
    }

    public async Task<FormularioCampoResponseDto?> GetByIdAsync(int id)
    {
        var entity = await context.FormularioCampos
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id && x.Activo);

        return entity is null ? null : _mapper.ToDto(entity);
    }

    public async Task UpdateAsync(int id, UpdateFormularioCampoDto dto)
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

    public async Task<List<FormularioCampoResponseDto>> GetByFormularioIdAsync(int formularioId)
    {
        return await context.FormularioCampos
            .AsNoTracking()
            .Where(x => x.Activo && x.FormularioId == formularioId)
            .OrderBy(x => x.Seccion)
            .ThenBy(x => x.Orden)
            .Select(x => _mapper.ToDto(x))
            .ToListAsync();
    }

    private async Task<FormularioCampo> GetEntityByIdAsync(int id)
    {
        var entity = await context.FormularioCampos
            .FirstOrDefaultAsync(x => x.Id == id);

        return entity ?? throw new NotFoundException("Campo de formulario no encontrado.");
    }

    private async Task EnsureFormularioExistsAsync(int formularioId)
    {
        var exists = await context.Formularios
            .AnyAsync(x => x.Id == formularioId && x.Activo);

        if (!exists)
            throw new NotFoundException("El formulario no existe.");
    }

    private async Task ValidateNombreCampoAsync(string nombreCampo, int formularioId, int? excludeId = null)
    {
        var exists = await context.FormularioCampos.AnyAsync(x =>
            x.NombreCampo == nombreCampo &&
            x.FormularioId == formularioId &&
            (!excludeId.HasValue || x.Id != excludeId.Value));

        if (exists)
            throw new ConflictException($"Ya existe un campo con el nombre '{nombreCampo}' en este formulario.");
    }
}
