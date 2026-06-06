namespace Hospital.Application.DTOs.Auth;

public sealed record LoginResponseDto(
    string AccessToken,
    string RefreshToken,
    DateTime ExpiresAt,
    UserDto User
);

public sealed record UserDto(
    int Id,
    string UserName,
    string NombreCompleto,
    List<string> Roles
);