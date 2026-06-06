using Hospital.Application.DTOs.Formularios;
using Hospital.Application.Interfaces.Common;

namespace Hospital.Application.Interfaces;

public interface IFormularioService
    : ICrudService<CreateFormularioDto, UpdateFormularioDto, FormularioResponseDto>
{
    Task<List<FormularioResponseDto>> GetByTipoAtencionIdAsync(int tipoAtencionId);
}
