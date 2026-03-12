using Microscope.Boilerplate.Clients.Web.Blazor.Material.Settings;
using Microscope.Boilerplate.Clients.Web.Shared.Services;
using Microsoft.JSInterop;
using MudBlazor;

namespace Microscope.Management.Clients.Web.Blazor.Material.Settings;

public class MaterialThemeService(IPreferenceService preferenceService, IJSRuntime jsRuntime)
{
    public bool IsDarkMode { get; set; }
    public MudTheme AppTheme { get; private set; } = Theme.MicroscopeTheme;
    public LuminanceMode LuminanceMode { get; private set; } = LuminanceMode.System;
    public event Action? OnChange;
    private void NotifyChange() => OnChange?.Invoke();
    public async Task InitializeAsync()
    {
        await SetThemeFromPreferences();
        await SetLuminanceFromPreferences();
        ApplyLuminance();
    }

    public async Task SetTheme(string theme)
    {
        AppTheme = GetThemeFromName(theme);
        await jsRuntime.InvokeVoidAsync("jsInterop.setCookie", "Theme", theme);
        NotifyChange();
    }
    
    public async Task SetLuminance(LuminanceMode mode)
    {
        LuminanceMode = mode;
        await jsRuntime.InvokeVoidAsync("jsInterop.setCookie", "LuminanceMode", mode);
    }
    
    public void SetDarkMode(bool isDarkMode)
    {
        IsDarkMode = isDarkMode;
        NotifyChange();
    }
    
    private async Task SetThemeFromPreferences()
    {
        var preferredTheme = await preferenceService.GetThemeAsync();
        if (!string.IsNullOrEmpty(preferredTheme))
            AppTheme = GetThemeFromName(preferredTheme);
    }
    
    private async Task SetLuminanceFromPreferences()
    {
        var preferredLuminanceMode = await preferenceService.GetLuminanceModeAsync();
        if (preferredLuminanceMode.HasValue)
            LuminanceMode = preferredLuminanceMode.Value;
    }
    
    public void ApplyLuminance()
    {
        IsDarkMode = LuminanceMode == LuminanceMode.Dark;
        NotifyChange();
    }
    
    private static MudTheme GetThemeFromName(string theme)
    {
        return theme switch
        {
            nameof(Theme.MicroscopeTheme) => Theme.MicroscopeTheme,
            nameof(Theme.SkylineTheme) => Theme.SkylineTheme,
            nameof(Theme.MudTheme) => Theme.MudTheme,
            _ => Theme.MicroscopeTheme
        };
    }
}