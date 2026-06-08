using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Hospital.Application.DTOs.Auth;
using Hospital.Application.Exceptions;
using Hospital.Application.Interfaces;
using Hospital.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Hospital.Infrastructure.Services;

public class AuthService(
    UserManager<ApplicationUser> userManager,
    IConfiguration configuration
) : IAuthService
{
    public async Task<LoginResponseDto?> LoginAsync(LoginDto dto)
    {
        var user = await userManager.FindByNameAsync(dto.UserName);

        if (user is null || !user.Activo)
            throw new UnauthorizedException("Usuario o contraseña incorrectos.");

        var validPassword = await userManager.CheckPasswordAsync(user, dto.Password);

        if (!validPassword)
            throw new UnauthorizedException("Usuario o contraseña incorrectos.");

        return await GenerateAuthResponseAsync(user);
    }


    public async Task<LoginResponseDto?> RefreshAsync(
        RefreshTokenDto dto)
    {
        var user = await userManager.Users
            .FirstOrDefaultAsync(x =>
                x.RefreshToken == dto.RefreshToken);

        if (user is null)
            throw new UnauthorizedAccessException("Refresh token inválido.");

        if (user.RefreshTokenExpiresAt is null ||
            user.RefreshTokenExpiresAt <= DateTime.UtcNow)
            throw new UnauthorizedAccessException("Refresh token expirado.");

        return await GenerateAuthResponseAsync(user);
    }

    public async Task<MeResponseDto?> MeAsync(int userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());

        if (user is null || !user.Activo)
            throw new UnauthorizedException("Usuario no autorizado.");

        var roles = await userManager.GetRolesAsync(user);

        return new MeResponseDto
        {
            UserId = user.Id,
            UserName = user.UserName ?? "",
            NombreCompleto = user.NombreCompleto,
            Roles = roles.ToList()
        };
    }

    public async Task LogoutAsync(int userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());

        if (user is null)
            return;

        user.RefreshToken = null;
        user.RefreshTokenExpiresAt = null;

        await userManager.UpdateAsync(user);
    }

    private async Task<LoginResponseDto> GenerateAuthResponseAsync(ApplicationUser user)
    {
        var roles = await userManager.GetRolesAsync(user);

        var accessTokenExpiresAt = DateTime.UtcNow.AddMinutes(30);
        var refreshTokenExpiresAt = DateTime.UtcNow.AddDays(7);

        var accessToken = GenerateJwtToken(
            user,
            roles.ToList(),
            accessTokenExpiresAt);

        var refreshToken = GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiresAt = refreshTokenExpiresAt;

        await userManager.UpdateAsync(user);

        return new LoginResponseDto(
            AccessToken: accessToken,
            RefreshToken: refreshToken,
            ExpiresAt: accessTokenExpiresAt,
            User: new UserDto(
                Id: user.Id,
                UserName: user.UserName ?? string.Empty,
                NombreCompleto: user.NombreCompleto,
                Roles: roles.ToList()
            )
        );
    }

    private string GenerateJwtToken(
        ApplicationUser user,
        List<string> roles,
        DateTime expiresAt)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName ?? string.Empty),
            new(ClaimTypes.Email, user.Email ?? string.Empty),
            new("nombreCompleto", user.NombreCompleto)
        };
        claims.AddRange(
            roles.Select(role => new Claim("role", role))
        );

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)
        );

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: expiresAt,
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static string GenerateRefreshToken()
    {
        return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
    }
}