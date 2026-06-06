using Hospital.Application.Common;
using Hospital.Application.DTOs.Pacientes;
using Hospital.Application.Exceptions;
using Hospital.Application.Interfaces;
using Hospital.Domain.Entities;
using Hospital.Infrastructure.Data;
using Hospital.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Services;

public class PacienteService(AppDbContext context) : IPacienteService
{
    private readonly PacienteMapper _mapper = new();

    public async Task<int> CreateAsync(CreatePacienteDto dto)
    {
        await ValidateCatalogosAsync(
            dto.TipoDocumentoId,
            dto.ExtensionDocumentoId,
            dto.SexoId,
            dto.EstadoCivilId
        );

        var existsDocumento = await ExistsDocumentoAsync(
            dto.TipoDocumentoId,
            dto.NumeroDocumento,
            dto.ComplementoDocumento,
            dto.ExtensionDocumentoId
        );

        if (existsDocumento)
            throw new ConflictException("El documento ya existe.");

        var existsCodigo = await context.Pacientes
            .AnyAsync(x =>
                x.Activo &&
                x.CodigoPaciente == dto.CodigoPaciente);

        if (existsCodigo)
            throw new ConflictException("El código de paciente ya existe.");

        var paciente = _mapper.ToEntity(dto);

        context.Pacientes.Add(paciente);

        await context.SaveChangesAsync();

        return paciente.Id;
    }

    public async Task<PagedResult<PacienteResponseDto>> GetPagedAsync(
        PagedQuery query)
    {
        var q = context.Pacientes
            .AsNoTracking()
            .Include(x => x.TipoDocumento)
            .Include(x => x.ExtensionDocumento)
            .Include(x => x.Sexo)
            .Include(x => x.EstadoCivil)
            .Where(x => x.Activo);

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            var search = query.Search.Trim();

            q = q.Where(x =>
                x.CodigoPaciente.Contains(search) ||
                x.Nombres.Contains(search) ||
                x.ApellidoPaterno.Contains(search) ||
                x.ApellidoMaterno.Contains(search) ||
                x.NumeroDocumento.Contains(search));
        }

        q = q.OrderBy(x => x.ApellidoPaterno)
            .ThenBy(x => x.Nombres);

        var totalCount = await q.CountAsync();

        var entities = await q
            .Skip(query.Skip)
            .Take(query.NormalizedPageSize)
            .ToListAsync();

        var items = entities
            .Select(MapToResponse)
            .ToList();

        return PagedResult<PacienteResponseDto>.Create(
            items,
            totalCount,
            query
        );
    }

    public async Task<List<PacienteResponseDto>> GetAllAsync()
    {
        var pacientes = await context.Pacientes
            .AsNoTracking()
            .Include(x => x.TipoDocumento)
            .Include(x => x.ExtensionDocumento)
            .Include(x => x.Sexo)
            .Include(x => x.EstadoCivil)
            .Where(x => x.Activo)
            .OrderBy(x => x.ApellidoPaterno)
            .ThenBy(x => x.Nombres)
            .ToListAsync();

        return pacientes
            .Select(MapToResponse)
            .ToList();
    }

    public async Task<PacienteResponseDto?> GetByIdAsync(int id)
    {
        var paciente = await context.Pacientes
            .AsNoTracking()
            .Include(x => x.TipoDocumento)
            .Include(x => x.ExtensionDocumento)
            .Include(x => x.Sexo)
            .Include(x => x.EstadoCivil)
            .FirstOrDefaultAsync(x =>
                x.Id == id &&
                x.Activo);

        return paciente is null
            ? null
            : MapToResponse(paciente);
    }

    public async Task UpdateAsync(
        int id,
        UpdatePacienteDto dto)
    {
        await ValidateCatalogosAsync(
            dto.TipoDocumentoId,
            dto.ExtensionDocumentoId,
            dto.SexoId,
            dto.EstadoCivilId
        );

        var entity = await context.Pacientes
            .FirstOrDefaultAsync(x =>
                x.Id == id &&
                x.Activo);

        if (entity is null)
            throw new NotFoundException("Paciente no encontrado.");

        var documentoExists = await ExistsDocumentoAsync(
            dto.TipoDocumentoId,
            dto.NumeroDocumento,
            dto.ComplementoDocumento,
            dto.ExtensionDocumentoId,
            id
        );

        if (documentoExists)
            throw new ConflictException("El documento ya existe.");

        _mapper.UpdateEntity(dto, entity);

        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await context.Pacientes
            .FirstOrDefaultAsync(x =>
                x.Id == id &&
                x.Activo);

        if (entity is null)
            throw new NotFoundException("Paciente no encontrado.");

        entity.Activo = false;

        await context.SaveChangesAsync();
    }

    private async Task<bool> ExistsDocumentoAsync(
        int tipoDocumentoId,
        string numeroDocumento,
        string? complementoDocumento,
        int? extensionDocumentoId,
        int? excludePacienteId = null)
    {
        var numero = numeroDocumento.Trim();

        var complemento = string.IsNullOrWhiteSpace(complementoDocumento)
            ? null
            : complementoDocumento.Trim();

        return await context.Pacientes.AnyAsync(x =>
            x.Activo &&
            x.TipoDocumentoId == tipoDocumentoId &&
            x.NumeroDocumento == numero &&
            x.ComplementoDocumento == complemento &&
            x.ExtensionDocumentoId == extensionDocumentoId &&
            (!excludePacienteId.HasValue ||
             x.Id != excludePacienteId.Value)
        );
    }

    private async Task ValidateCatalogosAsync(
        int tipoDocumentoId,
        int? extensionDocumentoId,
        int sexoId,
        int estadoCivilId)
    {
        await ValidateCatalogoItemAsync(
            tipoDocumentoId,
            "TIPO_DOCUMENTO"
        );

        if (extensionDocumentoId.HasValue)
        {
            await ValidateCatalogoItemAsync(
                extensionDocumentoId.Value,
                "EXTENSION_DOCUMENTO"
            );
        }

        await ValidateCatalogoItemAsync(
            sexoId,
            "SEXO"
        );

        await ValidateCatalogoItemAsync(
            estadoCivilId,
            "ESTADO_CIVIL"
        );
    }

    private async Task ValidateCatalogoItemAsync(
        int catalogoItemId,
        string grupoCodigo)
    {
        var exists = await context.CatalogoItems
            .AnyAsync(x =>
                x.Id == catalogoItemId &&
                x.Activo &&
                x.CatalogoGrupo.Codigo == grupoCodigo);

        if (!exists)
        {
            throw new BadRequestException(
                $"El catálogo '{grupoCodigo}' no es válido."
            );
        }
    }

    private PacienteResponseDto MapToResponse(Paciente paciente)
    {
        var dto = _mapper.ToDto(paciente);

        return dto with
        {
            TipoDocumento = paciente.TipoDocumento.Nombre,
            ExtensionDocumento = paciente.ExtensionDocumento?.Codigo,
            Sexo = paciente.Sexo.Nombre,
            EstadoCivil = paciente.EstadoCivil.Nombre,
            Edad = CalcularEdad(paciente.FechaNacimiento)
        };
    }

    private static int CalcularEdad(DateOnly fechaNacimiento)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);

        var edad = today.Year - fechaNacimiento.Year;

        if (fechaNacimiento > today.AddYears(-edad))
            edad--;

        return edad;
    }
}