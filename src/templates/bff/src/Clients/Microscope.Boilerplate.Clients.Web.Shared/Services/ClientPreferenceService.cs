using Microsoft.AspNetCore.Localization;
using Microsoft.JSInterop;

namespace Microscope.Boilerplate.Clients.Web.Shared.Services;

public class ClientPreferenceService(IJSRuntime jsRuntime) : IPreferenceService
{
    public async Task<string?> GetCultureAsync()
    {
        return await jsRuntime.InvokeAsync<string>("jsInterop.getCookie", CookieRequestCultureProvider.DefaultCookieName);
    }

    public async Task<LuminanceMode?> GetLuminanceModeAsync()
    {
        var value = await jsRuntime.InvokeAsync<string>("jsInterop.getCookie", "LuminanceMode");

        var result = Enum.TryParse<LuminanceMode>(value, out var lightMode);

        return result ? lightMode : null;
    }

    public async Task<string?> GetThemeAsync()
    {
        return await jsRuntime.InvokeAsync<string>("jsInterop.getCookie", "Theme");
    }
}