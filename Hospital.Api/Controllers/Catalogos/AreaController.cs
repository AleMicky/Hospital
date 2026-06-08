using Hospital.Application.Common;
using Hospital.Application.DTOs.Catalogos;
using Hospital.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Api.Controllers.Catalogos;

[Authorize(Policy = AuthorizationPolicies.Staff)]
[ApiController]
[Route("api/catalogos/areas")]
public class AreaController(IAreaService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAreaDto dto)
    {
        var id = await service.CreateAsync(dto);
        return Ok(new { id });
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery] PagedQuery query)
    {
        var result = await service.GetPagedAsync(query);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await service.GetByIdAsync(id);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("{id:int}/departamentos")]
    public async Task<IActionResult> GetDepartamentos(int id)
    {
        var result = await service.GetDepartamentosByAreaIdAsync(id);
        return Ok(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateAreaDto dto)
    {
        await service.UpdateAsync(id, dto);
        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await service.DeleteAsync(id);
        return Ok();
    }
}
