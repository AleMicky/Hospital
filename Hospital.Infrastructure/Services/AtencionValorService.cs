using Hospital.Application.Common;
using Hospital.Application.DTOs.Atenciones;
using Hospital.Application.Exceptions;
using Hospital.Application.Interfaces;
using Hospital.Domain.Entities;
using Hospital.Infrastructure.Data;
using Hospital.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Services;

public class AtencionValorService(AppDbContext context) : IAtencionValorService
{
    private readonly AtencionValorMapper _mapper = new();

    public async Task<int> CreateAsync(CreateAtencionValorDto dto)
    {
        await EnsureAtencionExistsAsync(dto.AtencionId);
        await EnsureFormularioCampoExistsAsync(dto.FormularioCampoId);
        await ValidateUniqueValorAsync(dto.AtencionId, dto.FormularioCampoId);

        var entity = _mapper.ToEntity(dto);
        context.AtencionValores.Add(entity);
        await context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<PagedResult<AtencionValorResponseDto>> GetPagedAsync(PagedQuery query)
    {
        var q = context.AtencionValores
            .AsNoTracking()
            .Where(x => x.Activo);

        var totalCount = await q.CountAsync();
        var entities = await q
            .Skip(query.Skip)
            .Take(query.NormalizedPageSize)
            .ToListAsync();

        var items = entities.Select(_mapper.ToDto).ToList();
        return PagedResult<AtencionValorResponseDto>.Create(items, totalCount, query);
    }

    public async Task<AtencionValorResponseDto?> GetByIdAsync(int id)
    {
        var entity = await context.AtencionValores
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id && x.Activo);

        return entity is null ? null : _mapper.ToDto(entity);
    }

    public async Task UpdateAsync(int id, UpdateAtencionValorDto dto)
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

    public async Task<List<AtencionValorResponseDto>> GetByAtencionIdAsync(int atencionId)
    {
        return await context.AtencionValores
            .AsNoTracking()
            .Where(x => x.Activo && x.AtencionId == atencionId)
            .Select(x => _mapper.ToDto(x))
            .ToListAsync();
    }

    private async Task<AtencionValor> GetEntityByIdAsync(int id)
    {
        var entity = await context.AtencionValores
            .FirstOrDefaultAsync(x => x.Id == id);

        return entity ?? throw new NotFoundException("Valor de atención no encontrado.");
    }

    private async Task EnsureAtencionExistsAsync(int atencionId)
    {
        var exists = await context.Atenciones
            .AnyAsync(x => x.Id == atencionId && x.Activo);

        if (!exists)
            throw new NotFoundException("La atención no existe.");
    }

    private async Task EnsureFormularioCampoExistsAsync(int formularioCampoId)
    {
        var exists = await context.FormularioCampos
            .AnyAsync(x => x.Id == formularioCampoId && x.Activo);

        if (!exists)
            throw new NotFoundException("El campo de formulario no existe.");
    }

    private async Task ValidateUniqueValorAsync(int atencionId, int formularioCampoId, int? excludeId = null)
    {
        var exists = await context.AtencionValores.AnyAsync(x =>
            x.AtencionId == atencionId &&
            x.FormularioCampoId == formularioCampoId &&
            (!excludeId.HasValue || x.Id != excludeId.Value));

        if (exists)
            throw new ConflictException("Ya existe un valor para este campo en la atención.");
    }
}
