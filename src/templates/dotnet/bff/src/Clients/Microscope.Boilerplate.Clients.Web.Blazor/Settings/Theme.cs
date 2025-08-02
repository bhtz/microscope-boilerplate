using MudBlazor;

namespace Microscope.Boilerplate.Clients.Web.Blazor.Settings;

public class Theme
{
    public static MudTheme DefaultTheme = new MudTheme()
    {
        PaletteLight = new PaletteLight()
        {
            Primary = "#464949",
            Secondary = "#fcf9ee",
            Info = "#213c43",
            Background = "#fcf9ee",
            AppbarBackground = Colors.Gray.Lighten5,
            AppbarText = "rgba(0,0,0, 0.7)",
            DrawerBackground = "#FFF",
            DrawerText = "rgba(0,0,0, 0.7)",
            Success = "#06d79c",
            Surface = "#ebd8c6", //Colors.Shades.White,
        },
        PaletteDark = new PaletteDark()
        {
            Black = "#27272f",
            Background = "#32333d",
            Primary = Colors.Cyan.Darken3,
            Info = "#e2c6ac",
            Surface = "#213c43",
            DrawerBackground = "#27272f",
            DrawerText = "rgba(255,255,255, 0.50)",
            AppbarBackground = "#27272f",
            AppbarText = "rgba(255,255,255, 0.70)",
            TextPrimary = "rgba(255,255,255, 0.70)",
            TextSecondary = "rgba(255,255,255, 0.50)",
            ActionDefault = "#ffffff",
            ActionDisabled = "rgba(255,255,255, 0.26)",
            ActionDisabledBackground = "rgba(255,255,255, 0.12)",
            DrawerIcon = "rgba(255,255,255, 0.50)"
        }
    };
    
    // public static MudTheme DefaultTheme = new MudTheme()
    // {
    //     Palette = new Palette()
    //     {
    //         Primary = Colors.Cyan.Darken3,
    //         Secondary = Colors.Red.Darken3,
    //         Background = Colors.Grey.Lighten5,
    //         AppbarBackground = Colors.Cyan.Darken3,
    //         DrawerBackground = "#FFF",
    //         DrawerText = "rgba(0,0,0, 0.7)",
    //         Success = "#06d79c"
    //     }
    // };
    //
    // public static MudTheme DarkTheme = new MudTheme()
    // {
    //     Palette = new Palette()
    //     {
    //         Black = "#27272f",
    //         Background = "#32333d",
    //         BackgroundGrey = "#27272f",
    //         Primary = Colors.Cyan.Darken3,
    //         Surface = "#373740",
    //         DrawerBackground = "#27272f",
    //         DrawerText = "rgba(255,255,255, 0.50)",
    //         AppbarBackground = "#27272f",
    //         AppbarText = "rgba(255,255,255, 0.70)",
    //         TextPrimary = "rgba(255,255,255, 0.70)",
    //         TextSecondary = "rgba(255,255,255, 0.50)",
    //         ActionDefault = "#ffffff",
    //         ActionDisabled = "rgba(255,255,255, 0.26)",
    //         ActionDisabledBackground = "rgba(255,255,255, 0.12)",
    //         DrawerIcon = "rgba(255,255,255, 0.50)"
    //     }
    // };
}