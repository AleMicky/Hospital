using Hospital.Application.Common;
using Hospital.Application.DTOs.Roles;
using Hospital.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Api.Controllers;

[Authorize(Policy = AuthorizationPolicies.AdminOnly)]
[ApiController]
[Route("api/roles")]
public class RolesController(IRoleService roleService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRoleDto dto)
    {
        var id = await roleService.CreateAsync(dto);
        return Ok(new { id });
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery] PagedQuery query)
    {
        var result = await roleService.GetPagedAsync(query);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await roleService.GetByIdAsync(id);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateRoleDto dto)
    {
        await roleService.UpdateAsync(id, dto);
        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await roleService.DeleteAsync(id);
        return Ok();
    }
}
