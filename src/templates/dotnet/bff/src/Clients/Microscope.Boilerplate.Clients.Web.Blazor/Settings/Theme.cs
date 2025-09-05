using MudBlazor;

namespace Microscope.Boilerplate.Clients.Web.Blazor.Settings;

public class Theme
{
    public static readonly MudTheme SkylineTheme = new MudTheme()
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

    public static readonly MudTheme DefaultTheme = new MudTheme()
    {
        PaletteLight = new PaletteLight()
        {
            Primary = Colors.Cyan.Darken3,
            Secondary = Colors.Red.Darken3,
            Background = Colors.Gray.Lighten5,
            AppbarBackground = Colors.Cyan.Darken3,
            DrawerBackground = "#FFF",
            DrawerText = "rgba(0,0,0, 0.7)",
            Success = "#06d79c"
        },

        PaletteDark = new PaletteDark()
        {
            Black = "#27272f",
            Background = "#32333d",
            BackgroundGray = "#27272f",
            Primary = Colors.Cyan.Darken3,
            Surface = "#373740",
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

    public static readonly MudTheme DefaultMudTheme = new MudTheme()
    {
        PaletteLight = new()
        {
            Black = "#110e2d",
            AppbarText = "#424242",
            AppbarBackground = "rgba(255,255,255,0.8)",
            DrawerBackground = "#ffffff",
            GrayLight = "#e8e8e8",
            GrayLighter = "#f9f9f9",
        },

        PaletteDark = new()
        {
            Primary = "#7e6fff",
            Surface = "#1e1e2d",
            Background = "#1a1a27",
            BackgroundGray = "#151521",
            AppbarText = "#92929f",
            AppbarBackground = "rgba(26,26,39,0.8)",
            DrawerBackground = "#1a1a27",
            ActionDefault = "#74718e",
            ActionDisabled = "#9999994d",
            ActionDisabledBackground = "#605f6d4d",
            TextPrimary = "#b2b0bf",
            TextSecondary = "#92929f",
            TextDisabled = "#ffffff33",
            DrawerIcon = "#92929f",
            DrawerText = "#92929f",
            GrayLight = "#2a2833",
            GrayLighter = "#1e1e2d",
            Info = "#4a86ff",
            Success = "#3dcb6c",
            Warning = "#ffb545",
            Error = "#ff3f5f",
            LinesDefault = "#33323e",
            TableLines = "#33323e",
            Divider = "#292838",
            OverlayLight = "#1e1e2d80",
        }
    };
}