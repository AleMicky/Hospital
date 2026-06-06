using Hospital.Application.Common;
using Hospital.Application.DTOs.Catalogo;
using Hospital.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Api.Controllers;

[Authorize(Policy = AuthorizationPolicies.Staff)]
[ApiController]
[Route("api/catalogo-grupos")]
public class CatalogoGrupoController(
    ICatalogoGrupoService service,
    ICatalogoItemService catalogoItemService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCatalogoGrupoDto dto)
    {
        var id = await service.CreateAsync(dto);
        return Ok(new { id });
    }

    [HttpGet("items")]
    public async Task<IActionResult> GroupByCatalogoItem()
    {
        var result = await service.GroupByCatalogoItemAsync();
        return Ok(result);
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

    [HttpGet("{id:int}/catalogo-items")]
    public async Task<IActionResult> GetItems(int id)
    {
        var result = await catalogoItemService.GetByCatalogoGrupoIdAsync(id);
        return Ok(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCatalogoGrupoDto dto)
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
