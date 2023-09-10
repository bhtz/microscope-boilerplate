namespace Microscope.Boilerplate.Clients.Web.Blazor.Settings;

public record Preference
{
    public bool IsDarkMode { get; set; }
    public bool IsRTL { get; set; }
    public bool IsDrawerOpen { get; set; }
    public string? LanguageCode { get; set; } = "fr-FR";
}