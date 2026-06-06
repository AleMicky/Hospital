using Hospital.Application.DTOs.Formularios;
using Hospital.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Hospital.Infrastructure.Mappings;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None)]
public partial class FormularioMapper
{
    public partial Formulario ToEntity(CreateFormularioDto dto);

    [MapperIgnoreTarget(nameof(Formulario.Id))]
    [MapperIgnoreTarget(nameof(Formulario.TipoAtencionId))]
    public partial void UpdateEntity(UpdateFormularioDto dto, Formulario entity);

    public partial FormularioResponseDto ToDto(Formulario entity);
}
