using Hospital.Application.DTOs.Catalogo;
using Hospital.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Hospital.Infrastructure.Mappings;

[Mapper(
    RequiredMappingStrategy = RequiredMappingStrategy.None
)]
public partial class CatalogoItemMapper
{
    public partial CatalogoItem ToEntity(CreateCatalogoItemDto dto);

    [MapperIgnoreTarget(nameof(CatalogoItem.Id))]
    [MapperIgnoreTarget(nameof(CatalogoItem.CatalogoGrupoId))]
    public partial void UpdateEntity(UpdateCatalogoItemDto dto, CatalogoItem entity);

    public partial CatalogoItemResponseDto ToDto(CatalogoItem entity);
}