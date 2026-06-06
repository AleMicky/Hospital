using Hospital.Application.DTOs.Atenciones;
using Hospital.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Hospital.Infrastructure.Mappings;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None)]
public partial class AtencionValorMapper
{
    public partial AtencionValor ToEntity(CreateAtencionValorDto dto);

    [MapperIgnoreTarget(nameof(AtencionValor.Id))]
    [MapperIgnoreTarget(nameof(AtencionValor.AtencionId))]
    [MapperIgnoreTarget(nameof(AtencionValor.FormularioCampoId))]
    public partial void UpdateEntity(UpdateAtencionValorDto dto, AtencionValor entity);

    public partial AtencionValorResponseDto ToDto(AtencionValor entity);
}
