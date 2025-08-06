using System.Security.Claims;

namespace Microscope.Boilerplate.Framework.Services;

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
