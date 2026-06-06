using Hospital.Application.Common;
using Hospital.Application.DTOs.Users;
using Hospital.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Api.Controllers;

[Authorize(Policy = AuthorizationPolicies.AdminOnly)]
[ApiController]
[Route("api/users")]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
    {
        var id = await userService.CreateAsync(dto);
        return Ok(new { id });
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery] PagedQuery query)
    {
        var result = await userService.GetPagedAsync(query);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await userService.GetByIdAsync(id);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto dto)
    {
        await userService.UpdateAsync(id, dto);
        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await userService.DeleteAsync(id);
        return Ok();
    }
}
