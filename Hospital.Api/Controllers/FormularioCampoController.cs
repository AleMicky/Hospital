using Hospital.Application.Interfaces;
using Hospital.Api.Controllers.Common;
using Hospital.Application.DTOs.Formularios;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Api.Controllers;

[Route("api/formulario-campos")]
public class FormularioCampoController(IFormularioCampoService service) :
    CrudController<CreateFormularioCampoDto, UpdateFormularioCampoDto, FormularioCampoResponseDto>(service)
{
    [HttpGet("por-formulario/{formularioId:int}")]
    public async Task<IActionResult> GetByFormularioId(int formularioId)
    {
        var result = await service.GetByFormularioIdAsync(formularioId);
        return Ok(result);
    }
}
