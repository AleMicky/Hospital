using Hospital.Application.Common;

namespace Hospital.Application.Interfaces.Common;

public interface ICrudService<in TCreateDto, in TUpdateDto, TResponseDto>
{
    Task<int> CreateAsync(TCreateDto dto);
    Task<PagedResult<TResponseDto>> GetPagedAsync(PagedQuery query);
    Task<TResponseDto?> GetByIdAsync(int id);
    Task UpdateAsync(int id, TUpdateDto dto);
    Task DeleteAsync(int id);
}