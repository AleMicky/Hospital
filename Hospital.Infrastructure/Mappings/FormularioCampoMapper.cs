using Hospital.Application.DTOs.Formularios;
using Hospital.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Hospital.Infrastructure.Mappings;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None)]
public partial class FormularioCampoMapper
{
    public partial FormularioCampo ToEntity(CreateFormularioCampoDto dto);

    [MapperIgnoreTarget(nameof(FormularioCampo.Id))]
    [MapperIgnoreTarget(nameof(FormularioCampo.FormularioId))]
    [MapperIgnoreTarget(nameof(FormularioCampo.NombreCampo))]
    public partial void UpdateEntity(UpdateFormularioCampoDto dto, FormularioCampo entity);

    public partial FormularioCampoResponseDto ToDto(FormularioCampo entity);
}
