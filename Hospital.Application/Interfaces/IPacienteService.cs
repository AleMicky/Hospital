using Hospital.Application.Common;
using Hospital.Application.DTOs.Pacientes;

namespace Hospital.Application.Interfaces;

public interface IPacienteService
{
    Task<int> CreateAsync(CreatePacienteDto dto);
    Task<PagedResult<PacienteResponseDto>> GetPagedAsync(PagedQuery query);
    Task<PacienteResponseDto?> GetByIdAsync(int id);
    Task UpdateAsync(int id, UpdatePacienteDto dto);
    Task DeleteAsync(int id);
}