using Hospital.Application.DTOs.Atenciones;
using Hospital.Application.Interfaces.Common;

namespace Hospital.Application.Interfaces;

public interface IAtencionValorService
    : ICrudService<CreateAtencionValorDto, UpdateAtencionValorDto, AtencionValorResponseDto>
{
    Task<List<AtencionValorResponseDto>> GetByAtencionIdAsync(int atencionId);
}
