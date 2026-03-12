using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.JSInterop;

namespace Microscope.Boilerplate.Clients.Web.Shared.Services;

public class CookieService(IJSRuntime js, IHttpContextAccessor httpContextAccessor)
{
    public async Task<string?> GetCultureFromCookie()
    {
        var cookie = httpContextAccessor.HttpContext?.Request.Cookies[CookieRequestCultureProvider.DefaultCookieName];
        // return await js.InvokeAsync<string>("jsInterop.getCookie", CookieRequestCultureProvider.DefaultCookieName);
        return cookie;
    }
}