using Hospital.Application.DTOs.Atenciones;
using Hospital.Application.Interfaces.Common;

namespace Hospital.Application.Interfaces;

public interface IAtencionService
    : ICrudService<CreateAtencionDto, UpdateAtencionDto, AtencionResponseDto>
{
    Task<List<AtencionResponseDto>> GetByPacienteIdAsync(int pacienteId);
}
