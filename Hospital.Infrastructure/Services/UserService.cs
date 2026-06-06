using Hospital.Application.Common;
using Hospital.Application.DTOs.Users;
using Hospital.Application.Exceptions;
using Hospital.Application.Interfaces;
using Hospital.Infrastructure.Identity;
using Hospital.Infrastructure.Mappings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Services;

public class UserService(
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole<int>> roleManager)
    : IUserService
{
    private readonly UserMapper _mapper = new();

    public async Task<int> CreateAsync(CreateUserDto dto)
    {
        var existsUser = await userManager.FindByNameAsync(dto.UserName);

        if (existsUser is not null)
            throw new ConflictException("El usuario ya existe.");

        if (!await roleManager.RoleExistsAsync(dto.Rol))
            throw new BadRequestException("El rol no existe.");

        var user = _mapper.ToEntity(dto);
        user.Activo = true;

        var result = await userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new BadRequestException(errors);
        }

        await userManager.AddToRoleAsync(user, dto.Rol);

        return user.Id;
    }

    public async Task<PagedResult<UserResponseDto>> GetPagedAsync(PagedQuery query)
    {
        var q = userManager.Users.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            var search = query.Search.Trim();
            q = q.Where(x =>
                (x.UserName != null && x.UserName.Contains(search)) ||
                x.NombreCompleto.Contains(search));
        }

        q = q.OrderBy(x => x.UserName);

        var totalCount = await q.CountAsync();
        var users = await q
            .Skip(query.Skip)
            .Take(query.NormalizedPageSize)
            .ToListAsync();

        var items = new List<UserResponseDto>();
        foreach (var user in users)
            items.Add(await MapToResponseAsync(user));

        return PagedResult<UserResponseDto>.Create(items, totalCount, query);
    }

    public async Task<UserResponseDto?> GetByIdAsync(int id)
    {
        var user = await userManager.FindByIdAsync(id.ToString());
        return user is null ? null : await MapToResponseAsync(user);
    }

    public async Task UpdateAsync(int id, UpdateUserDto dto)
    {
        var user = await GetUserByIdAsync(id);

        if (!await roleManager.RoleExistsAsync(dto.Rol))
            throw new BadRequestException("El rol no existe.");

        _mapper.UpdateEntity(dto, user);

        if (!string.IsNullOrWhiteSpace(dto.Password))
        {
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var passwordResult = await userManager.ResetPasswordAsync(user, token, dto.Password);

            if (!passwordResult.Succeeded)
            {
                var errors = string.Join(", ", passwordResult.Errors.Select(e => e.Description));
                throw new BadRequestException(errors);
            }
        }

        var updateResult = await userManager.UpdateAsync(user);

        if (!updateResult.Succeeded)
        {
            var errors = string.Join(", ", updateResult.Errors.Select(e => e.Description));
            throw new BadRequestException(errors);
        }

        var currentRoles = await userManager.GetRolesAsync(user);
        await userManager.RemoveFromRolesAsync(user, currentRoles);
        await userManager.AddToRoleAsync(user, dto.Rol);
    }

    public async Task DeleteAsync(int id)
    {
        var user = await GetUserByIdAsync(id);
        user.Activo = false;
        user.RefreshToken = null;
        user.RefreshTokenExpiresAt = null;

        var result = await userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new BadRequestException(errors);
        }
    }

    private async Task<ApplicationUser> GetUserByIdAsync(int id)
    {
        var user = await userManager.FindByIdAsync(id.ToString());
        return user ?? throw new NotFoundException("Usuario no encontrado.");
    }

    private async Task<UserResponseDto> MapToResponseAsync(ApplicationUser user)
    {
        var dto = _mapper.ToDto(user);
        dto.Roles = (await userManager.GetRolesAsync(user)).ToList();
        return dto;
    }
}
