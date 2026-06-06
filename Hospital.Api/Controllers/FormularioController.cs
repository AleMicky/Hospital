using Hospital.Application.Interfaces;
using Hospital.Api.Controllers.Common;
using Hospital.Application.DTOs.Formularios;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Api.Controllers;

[Route("api/formularios")]
public class FormularioController(IFormularioService service) :
    CrudController<CreateFormularioDto, UpdateFormularioDto, FormularioResponseDto>(service)
{
    [HttpGet("por-tipo/{tipoAtencionId:int}")]
    public async Task<IActionResult> GetByTipoAtencionId(int tipoAtencionId)
    {
        var result = await service.GetByTipoAtencionIdAsync(tipoAtencionId);
        return Ok(result);
    }
}
