using Hospital.Application.Common;
using Hospital.Application.DTOs.Pacientes;
using Hospital.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Api.Controllers;

[Authorize(Policy = AuthorizationPolicies.Staff)]
[ApiController]
[Route("api/pacientes")]
public class PacientesController(IPacienteService pacienteService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePacienteDto dto)
    {
        var id = await pacienteService.CreateAsync(dto);
        return Ok(new { id });
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery] PagedQuery query)
    {
        var result = await pacienteService.GetPagedAsync(query);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await pacienteService.GetByIdAsync(id);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePacienteDto dto)
    {
        await pacienteService.UpdateAsync(id, dto);
        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await pacienteService.DeleteAsync(id);
        return Ok();
    }
}
