using Hospital.Application.DTOs.Atenciones;
using Hospital.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Hospital.Infrastructure.Mappings;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None)]
public partial class AtencionMapper
{
    public partial Atencion ToEntity(CreateAtencionDto dto);

    [MapperIgnoreTarget(nameof(Atencion.Id))]
    [MapperIgnoreTarget(nameof(Atencion.PacienteId))]
    [MapperIgnoreTarget(nameof(Atencion.TipoAtencionId))]
    [MapperIgnoreTarget(nameof(Atencion.FormularioId))]
    public partial void UpdateEntity(UpdateAtencionDto dto, Atencion entity);

    public partial AtencionResponseDto ToDto(Atencion entity);
}
