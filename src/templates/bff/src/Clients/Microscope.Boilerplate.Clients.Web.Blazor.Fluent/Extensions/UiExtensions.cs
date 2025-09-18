using Microscope.Boilerplate.Clients.Web.Shared.Services;
using Microsoft.FluentUI.AspNetCore.Components;

namespace Microscope.Boilerplate.Clients.Web.Shared.Configurations;

public static class UiConfiguration
{
    public static IServiceCollection AddFluentUiConfiguration(this IServiceCollection services)
    {
        services.AddScoped<HostingEnvironmentService>();
        services.AddFluentUIComponents();

        return services;
    }
}