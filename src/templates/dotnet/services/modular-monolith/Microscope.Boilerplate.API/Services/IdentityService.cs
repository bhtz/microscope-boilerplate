using System.Security.Claims;
using Microscope.Boilerplate.Framework.Application.Services;

namespace Microscope.Boilerplate.API.Services;

public class IdentityService : IIdentityService
{
    private IHttpContextAccessor _context;

    public IdentityService(IHttpContextAccessor context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Guid GetUserId()
    {
        var id = _context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        return Guid.Parse(id);
    }

    public string GetTenantId()
    {
        return _context.HttpContext.User.FindFirst("iss").Value;
    }

    public string GetUserName()
    {
        return _context.HttpContext.User.FindFirst("name").Value;
    }

    public bool IsInRole(string role)
    {
        return _context.HttpContext.User.IsInRole(role);
    }

    public string GetUserMail()
    {
        return _context.HttpContext.User.FindFirst(ClaimTypes.Email).Value;
    }

    public ClaimsPrincipal GetClaimsPrincipal()
    {
        return _context.HttpContext.User;
    }

    public string GetToken()
    {
        return _context.HttpContext.Request.Headers["Authorization"];
    }
}
