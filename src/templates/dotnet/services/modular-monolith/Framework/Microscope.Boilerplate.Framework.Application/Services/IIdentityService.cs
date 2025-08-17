using System.Security.Claims;

namespace Microscope.Framework.Application.Services;

public interface IIdentityService
{
    string GetTenantId();
    Guid GetUserId();
    string GetUserName();
    string GetUserMail();
    bool IsInRole(string role);
    ClaimsPrincipal GetClaimsPrincipal();
    string GetToken();
}
