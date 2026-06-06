using Hospital.Application.Common;
using Hospital.Application.DTOs.Roles;
using Hospital.Application.Exceptions;
using Hospital.Application.Interfaces;
using Hospital.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Services;

public class RoleService(
    RoleManager<IdentityRole<int>> roleManager,
    UserManager<ApplicationUser> userManager) : IRoleService
{
    public async Task<int> CreateAsync(CreateRoleDto dto)
    {
        if (await roleManager.RoleExistsAsync(dto.Name))
            throw new ConflictException("El rol ya existe.");

        var role = new IdentityRole<int>(dto.Name);
        var result = await roleManager.CreateAsync(role);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new BadRequestException(errors);
        }

        return role.Id;
    }

    public async Task<PagedResult<RoleResponseDto>> GetPagedAsync(PagedQuery query)
    {
        var q = roleManager.Roles.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            var search = query.Search.Trim();
            q = q.Where(x => x.Name != null && x.Name.Contains(search));
        }

        q = q.OrderBy(x => x.Name);

        var totalCount = await q.CountAsync();
        var items = await q
            .Skip(query.Skip)
            .Take(query.NormalizedPageSize)
            .Select(role => new RoleResponseDto
            {
                Id = role.Id,
                Name = role.Name ?? ""
            })
            .ToListAsync();

        return PagedResult<RoleResponseDto>.Create(items, totalCount, query);
    }

    public async Task<RoleResponseDto?> GetByIdAsync(int id)
    {
        var role = await roleManager.FindByIdAsync(id.ToString());

        if (role is null)
            return null;

        return new RoleResponseDto
        {
            Id = role.Id,
            Name = role.Name ?? ""
        };
    }

    public async Task UpdateAsync(int id, UpdateRoleDto dto)
    {
        var role = await GetRoleByIdAsync(id);

        if (await roleManager.RoleExistsAsync(dto.Name) &&
            !string.Equals(role.Name, dto.Name, StringComparison.OrdinalIgnoreCase))
        {
            throw new ConflictException("El rol ya existe.");
        }

        role.Name = dto.Name;
        role.NormalizedName = dto.Name.ToUpperInvariant();

        var result = await roleManager.UpdateAsync(role);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new BadRequestException(errors);
        }
    }

    public async Task DeleteAsync(int id)
    {
        var role = await GetRoleByIdAsync(id);

        var usersInRole = await userManager.GetUsersInRoleAsync(role.Name!);

        if (usersInRole.Count > 0)
            throw new ConflictException("No se puede eliminar un rol asignado a usuarios.");

        var result = await roleManager.DeleteAsync(role);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new BadRequestException(errors);
        }
    }

    private async Task<IdentityRole<int>> GetRoleByIdAsync(int id)
    {
        var role = await roleManager.FindByIdAsync(id.ToString());
        return role ?? throw new NotFoundException("Rol no encontrado.");
    }
}
