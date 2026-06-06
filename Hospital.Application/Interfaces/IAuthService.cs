using Hospital.Application.DTOs.Auth;

namespace Hospital.Application.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDto?> LoginAsync(LoginDto dto);
    Task<LoginResponseDto?> RefreshAsync(RefreshTokenDto dto);
    Task<MeResponseDto?> MeAsync(int userId);
    Task LogoutAsync(int userId);
}