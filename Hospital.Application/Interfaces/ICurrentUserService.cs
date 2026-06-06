namespace Hospital.Application.Interfaces;

public interface ICurrentUserService
{
    bool IsAuthenticated { get; }
    int? UserId { get; }
    string UserName { get; }
    string AuditUserName { get; }
}
