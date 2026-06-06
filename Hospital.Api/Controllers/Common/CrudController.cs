using Hospital.Application.Common;
using Hospital.Application.Interfaces.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Api.Controllers.Common;

[Authorize(Policy = AuthorizationPolicies.Staff)]
[ApiController]
public abstract class CrudController<TCreateDto, TUpdateDto, TResponseDto>(
    ICrudService<TCreateDto, TUpdateDto, TResponseDto> service
) : ControllerBase
{
    [HttpPost]
    public virtual async Task<IActionResult> Create([FromBody] TCreateDto dto)
    {
        var id = await service.CreateAsync(dto);
        return Ok(new { id });
    }

    [HttpGet]
    public virtual async Task<IActionResult> GetPaged([FromQuery] PagedQuery query)
    {
        var result = await service.GetPagedAsync(query);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public virtual async Task<IActionResult> GetById(int id)
    {
        var result = await service.GetByIdAsync(id);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpPut("{id:int}")]
    public virtual async Task<IActionResult> Update(
        int id,
        [FromBody] TUpdateDto dto)
    {
        await service.UpdateAsync(id, dto);
        return Ok();
    }

    [HttpDelete("{id:int}")]
    public virtual async Task<IActionResult> Delete(int id)
    {
        await service.DeleteAsync(id);
        return Ok();
    }
}
