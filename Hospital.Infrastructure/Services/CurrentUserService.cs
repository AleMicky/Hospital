using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Hospital.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Hospital.Infrastructure.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    private const string SystemUser = "Sistema";

    private ClaimsPrincipal? User => httpContextAccessor.HttpContext?.User;

    public bool IsAuthenticated =>
        User?.Identity?.IsAuthenticated == true;

    public int? UserId
    {
        get
        {
            var id = User?.FindFirstValue(JwtRegisteredClaimNames.Sub)
                ?? User?.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.TryParse(id, out var userId) ? userId : null;
        }
    }

    public string UserName
    {
        get
        {
            if (!IsAuthenticated)
                return SystemUser;

            return User?.FindFirstValue(JwtRegisteredClaimNames.UniqueName)
                ?? User?.FindFirstValue(ClaimTypes.Name)
                ?? User?.Identity?.Name
                ?? SystemUser;
        }
    }

    public string AuditUserName => IsAuthenticated ? UserName : SystemUser;
}
