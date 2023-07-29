using Blazored.LocalStorage;
using Microscope.Boilerplate.Clients.Web.Blazor.Settings;
using MudBlazor;

namespace Microscope.Boilerplate.Clients.Web.Blazor.Services
{
public class PreferenceService
    {
        private readonly ILocalStorageService _localStorageService;

        public PreferenceService(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public async Task<bool> ToggleDarkModeAsync()
        {
            var preference = await GetPreference();
            preference.IsDarkMode = !preference.IsDarkMode;
            await SetPreference(preference);
            return !preference.IsDarkMode;
        }
        
        public async Task<bool> ToggleDrawerAsync()
        {
            var preference = await GetPreference();
            preference.IsDrawerOpen = !preference.IsDrawerOpen;
            await SetPreference(preference);
            return !preference.IsDrawerOpen;
        }
        
        public async Task<bool> GetDrawerPreferenceAsync()
        {
            var preference = await GetPreference();
            return preference.IsDrawerOpen;
        }

        public async Task ChangeLanguageAsync(string languageCode)
        {
            var preference = await GetPreference();
            preference.LanguageCode = languageCode;
            await SetPreference(preference);
        }

        public async Task<MudTheme> GetCurrentThemeAsync()
        {
            var preference = await GetPreference();
            if (preference.IsDarkMode) return Theme.DarkTheme;
            return Theme.DefaultTheme;
        }

        public async Task<Preference> GetPreference()
        {
            return await _localStorageService.GetItemAsync<Preference>("preference") ?? new Preference();
        }

        public async Task SetPreference(Preference preference)
        {
            await _localStorageService.SetItemAsync("preference", preference);
        }
    }
}