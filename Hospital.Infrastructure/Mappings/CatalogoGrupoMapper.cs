using Hospital.Application.DTOs.Catalogo;
using Hospital.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Hospital.Infrastructure.Mappings;

[Mapper(
    RequiredMappingStrategy = RequiredMappingStrategy.None
)]
public partial class CatalogoGrupoMapper
{
    public partial CatalogoGrupo ToEntity(
        CreateCatalogoGrupoDto dto
    );

    [MapperIgnoreTarget(nameof(CatalogoGrupo.Id))]
    [MapperIgnoreTarget(nameof(CatalogoGrupo.Codigo))]
    public partial void UpdateEntity(
        UpdateCatalogoGrupoDto dto,
        CatalogoGrupo entity
    );

    public partial CatalogoGrupoResponseDto ToDto(
        CatalogoGrupo entity
    );
}