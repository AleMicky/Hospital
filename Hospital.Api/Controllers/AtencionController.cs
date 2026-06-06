using Hospital.Application.Interfaces;
using Hospital.Api.Controllers.Common;
using Hospital.Application.DTOs.Atenciones;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Api.Controllers;

[Route("api/atenciones")]
public class AtencionController(IAtencionService service) :
    CrudController<CreateAtencionDto, UpdateAtencionDto, AtencionResponseDto>(service)
{
    [HttpGet("por-paciente/{pacienteId:int}")]
    public async Task<IActionResult> GetByPacienteId(int pacienteId)
    {
        var result = await service.GetByPacienteIdAsync(pacienteId);
        return Ok(result);
    }
}
