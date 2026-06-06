using Hospital.Application.Common;
using Hospital.Application.DTOs.Catalogo;

namespace Hospital.Application.Interfaces;

public interface ICatalogoGrupoService
{
    Task<int> CreateAsync(CreateCatalogoGrupoDto dto);
    Task<PagedResult<CatalogoGrupoResponseDto>> GetPagedAsync(PagedQuery query);
    Task<CatalogoGrupoResponseDto?> GetByIdAsync(int id);
    Task UpdateAsync(int id, UpdateCatalogoGrupoDto dto);
    Task DeleteAsync(int id);
    Task<List<CatalogoGrupoItemResponseDto>> GroupByCatalogoItemAsync();
}