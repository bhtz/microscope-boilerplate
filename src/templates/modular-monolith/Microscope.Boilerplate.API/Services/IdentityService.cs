using System.Security.Claims;
using Microscope.Boilerplate.Framework.Application.Services;

namespace Microscope.Boilerplate.API.Services;

public sealed class IdentityService : IIdentityService
{
    private readonly IHttpContextAccessor _context;

    public IdentityService(IHttpContextAccessor context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Guid GetUserId()
    {
        var idValue = _context.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Guid.TryParse(idValue, out var id) ? id : Guid.Empty;
    }

    public string GetTenantId()
    {
        return _context.HttpContext?.User?.FindFirst("iss")?.Value ?? string.Empty;
    }

    public string GetUserName()
    {
        return _context.HttpContext?.User?.FindFirst("name")?.Value
               ?? _context.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value
               ?? string.Empty;
    }

    public bool IsInRole(string role)
    {
        return _context.HttpContext?.User?.IsInRole(role) ?? false;
    }

    public string GetUserMail()
    {
        return _context.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;
    }

    public ClaimsPrincipal GetClaimsPrincipal()
    {
        return _context.HttpContext?.User ?? new ClaimsPrincipal();
    }

    public string GetToken()
    {
        var authorization = _context.HttpContext?.Request.Headers.Authorization.ToString();
        return string.IsNullOrWhiteSpace(authorization) ? string.Empty : authorization;
    }
}
