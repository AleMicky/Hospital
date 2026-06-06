using Hospital.Application.DTOs.Catalogo;
using Hospital.Application.Interfaces.Common;

namespace Hospital.Application.Interfaces;

public interface ICatalogoItemService
    : ICrudService<
        CreateCatalogoItemDto,
        UpdateCatalogoItemDto,
        CatalogoItemResponseDto>
{
    Task<List<CatalogoItemResponseDto>> GetByCatalogoGrupoIdAsync(int id);
}