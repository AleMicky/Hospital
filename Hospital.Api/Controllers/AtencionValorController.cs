using Hospital.Application.Interfaces;
using Hospital.Api.Controllers.Common;
using Hospital.Application.DTOs.Atenciones;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Api.Controllers;

[Route("api/atencion-valores")]
public class AtencionValorController(IAtencionValorService service) :
    CrudController<CreateAtencionValorDto, UpdateAtencionValorDto, AtencionValorResponseDto>(service)
{
    [HttpGet("por-atencion/{atencionId:int}")]
    public async Task<IActionResult> GetByAtencionId(int atencionId)
    {
        var result = await service.GetByAtencionIdAsync(atencionId);
        return Ok(result);
    }
}
