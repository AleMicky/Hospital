using Hospital.Application.DTOs.TiposAtencion;
using Hospital.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Hospital.Infrastructure.Mappings;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None)]
public partial class TipoAtencionMapper
{
    public partial TipoAtencion ToEntity(CreateTipoAtencionDto dto);

    [MapperIgnoreTarget(nameof(TipoAtencion.Id))]
    [MapperIgnoreTarget(nameof(TipoAtencion.Codigo))]
    public partial void UpdateEntity(UpdateTipoAtencionDto dto, TipoAtencion entity);

    public partial TipoAtencionResponseDto ToDto(TipoAtencion entity);
}
