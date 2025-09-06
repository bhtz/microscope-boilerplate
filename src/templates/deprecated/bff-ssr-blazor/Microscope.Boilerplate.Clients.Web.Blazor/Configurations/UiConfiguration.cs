using Microscope.Boilerplate.Clients.Web.Blazor.Services;
using MudBlazor;
using MudBlazor.Services;
using Mylight.SmartDigitalCoffee.SDK.GraphQL.Serializers;

namespace Microscope.Boilerplate.Clients.Web.Blazor.Configurations;

public static class UiConfiguration
{
    public static IServiceCollection AddUiConfiguration(this IServiceCollection services)
    {
        services.AddSerializer<Float8Serializer>();
        services.AddScoped<HostingEnvironmentService>();
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
