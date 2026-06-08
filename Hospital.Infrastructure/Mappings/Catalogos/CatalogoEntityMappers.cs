using Hospital.Application.DTOs.Catalogos;
using Hospital.Domain.Entities.Catalogos;
using Riok.Mapperly.Abstractions;

namespace Hospital.Infrastructure.Mappings.Catalogos;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None)]
public partial class AreaMapper
{
    public partial Area ToEntity(CreateAreaDto dto);

    [MapperIgnoreTarget(nameof(Area.Id))]
    public partial void UpdateEntity(UpdateAreaDto dto, Area entity);

    public partial AreaResponseDto ToDto(Area entity);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None)]
public partial class DepartamentoMapper
{
    public partial Departamento ToEntity(CreateDepartamentoDto dto);

    [MapperIgnoreTarget(nameof(Departamento.Id))]
    [MapperIgnoreTarget(nameof(Departamento.AreaId))]
    public partial void UpdateEntity(UpdateDepartamentoDto dto, Departamento entity);

    [MapProperty(nameof(Departamento.Area.Nombre), nameof(DepartamentoResponseDto.AreaNombre))]
    public partial DepartamentoResponseDto ToDto(Departamento entity);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None)]
public partial class ServicioMapper
{
    public partial Servicio ToEntity(CreateServicioDto dto);

    [MapperIgnoreTarget(nameof(Servicio.Id))]
    [MapperIgnoreTarget(nameof(Servicio.DepartamentoId))]
    public partial void UpdateEntity(UpdateServicioDto dto, Servicio entity);

    [MapProperty(nameof(Servicio.Departamento.Nombre), nameof(ServicioResponseDto.DepartamentoNombre))]
    public partial ServicioResponseDto ToDto(Servicio entity);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None)]
public partial class PrestacionMapper
{
    public partial Prestacion ToEntity(CreatePrestacionDto dto);

    [MapperIgnoreTarget(nameof(Prestacion.Id))]
    [MapperIgnoreTarget(nameof(Prestacion.ServicioId))]
    public partial void UpdateEntity(UpdatePrestacionDto dto, Prestacion entity);

    [MapProperty(nameof(Prestacion.Servicio.Nombre), nameof(PrestacionResponseDto.ServicioNombre))]
    public partial PrestacionResponseDto ToDto(Prestacion entity);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None)]
public partial class EspecialidadMapper
{
    public partial Especialidad ToEntity(CreateEspecialidadDto dto);

    [MapperIgnoreTarget(nameof(Especialidad.Id))]
    public partial void UpdateEntity(UpdateEspecialidadDto dto, Especialidad entity);

    public partial EspecialidadResponseDto ToDto(Especialidad entity);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None)]
public partial class ProfesionMapper
{
    public partial Profesion ToEntity(CreateProfesionDto dto);

    [MapperIgnoreTarget(nameof(Profesion.Id))]
    public partial void UpdateEntity(UpdateProfesionDto dto, Profesion entity);

    public partial ProfesionResponseDto ToDto(Profesion entity);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None)]
public partial class CargoMapper
{
    public partial Cargo ToEntity(CreateCargoDto dto);

    [MapperIgnoreTarget(nameof(Cargo.Id))]
    public partial void UpdateEntity(UpdateCargoDto dto, Cargo entity);

    public partial CargoResponseDto ToDto(Cargo entity);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None)]
public partial class TipoAtencionCatalogoMapper
{
    public partial TipoAtencion ToEntity(CreateTipoAtencionCatalogoDto dto);

    [MapperIgnoreTarget(nameof(TipoAtencion.Id))]
    public partial void UpdateEntity(UpdateTipoAtencionCatalogoDto dto, TipoAtencion entity);

    public partial TipoAtencionCatalogoResponseDto ToDto(TipoAtencion entity);
}
