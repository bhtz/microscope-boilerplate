using Microsoft.AspNetCore.Localization;

namespace Microscope.Boilerplate.Clients.Web.Shared.Services;

public class ServerPreferenceService(IHttpContextAccessor accessor) : IPreferenceService
{
    public Task<string?> GetCultureAsync()
    {
        var cookie = accessor.HttpContext?
            .Request.Cookies[CookieRequestCultureProvider.DefaultCookieName];

        if (cookie is null)
            return Task.FromResult<string?>(null);

        var parsed = CookieRequestCultureProvider.ParseCookieValue(cookie);
        return Task.FromResult(parsed?.Cultures.FirstOrDefault().Value);
    }

    public Task<LuminanceMode?> GetLuminanceModeAsync()
    {
        var value = accessor.HttpContext?.Request.Cookies["LuminanceMode"];
        var result = Enum.TryParse<LuminanceMode>(value, out var lightMode);
        
        return Task.FromResult<LuminanceMode?>(result ? lightMode : null);
    }

    public Task<string?> GetThemeAsync()
    {
        var value = accessor.HttpContext?.Request.Cookies["Theme"];
        return Task.FromResult(value);
    }
}