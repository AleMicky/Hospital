using Hospital.Application.DTOs.Formularios;
using Hospital.Application.Interfaces.Common;

namespace Hospital.Application.Interfaces;

public interface IFormularioCampoService
    : ICrudService<CreateFormularioCampoDto, UpdateFormularioCampoDto, FormularioCampoResponseDto>
{
    Task<List<FormularioCampoResponseDto>> GetByFormularioIdAsync(int formularioId);
}
