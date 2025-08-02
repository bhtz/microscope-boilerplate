using ApexCharts;
using Microscope.Boilerplate.Clients.SDK.GraphQL.Serializers;
using Microscope.Boilerplate.Clients.Web.Blazor.Services;
using MudBlazor;
using MudBlazor.Services;

namespace Microscope.Boilerplate.Clients.Web.Blazor.Configurations;

public static class UiConfiguration
{
    public static IServiceCollection AddUiConfiguration(this IServiceCollection services)
    {
        services.AddSerializer<Float8Serializer>();
        services.AddScoped<HostingEnvironmentService>();
        services.AddApexCharts(e =>
        {
            e.GlobalOptions = new ApexChartBaseOptions
            {
                Theme = new Theme { Palette = PaletteType.Palette1 }
            };
        });
        services.AddMudServices(config =>
        {
            config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
            config.SnackbarConfiguration.PreventDuplicates = false;
            config.SnackbarConfiguration.NewestOnTop = false;
            config.SnackbarConfiguration.ShowCloseIcon = true;
            config.SnackbarConfiguration.VisibleStateDuration = 3000;
            config.SnackbarConfiguration.HideTransitionDuration = 100;
            config.SnackbarConfiguration.ShowTransitionDuration = 100;
            config.SnackbarConfiguration.SnackbarVariant = Variant.Outlined;
        });

        return services;
    }
}