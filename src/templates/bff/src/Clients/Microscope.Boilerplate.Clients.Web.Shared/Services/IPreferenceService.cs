namespace Microscope.Boilerplate.Clients.Web.Shared.Services;

public interface IPreferenceService
{
    Task<string?> GetCultureAsync();
    Task<LuminanceMode?> GetLuminanceModeAsync();
    Task<string?> GetThemeAsync();
}

public enum LuminanceMode
{
    System = 1,
    Light = 2,
    Dark = 3
}