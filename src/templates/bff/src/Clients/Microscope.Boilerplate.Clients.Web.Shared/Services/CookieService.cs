using Microsoft.AspNetCore.Localization;
using Microsoft.JSInterop;

namespace Microscope.Boilerplate.Clients.Web.Shared.Services;

public class CookieService(IJSRuntime js)
{
    public async Task<string> GetCultureFromCookie()
    {
        return await js.InvokeAsync<string>("jsInterop.getCookie", CookieRequestCultureProvider.DefaultCookieName);
    }
}