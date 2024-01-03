using Blazored.LocalStorage;
using Microscope.Boilerplate.Clients.Web.Blazor.Extensions;
using Microscope.Boilerplate.Clients.Web.Blazor.Services;
using MudBlazor;
using MudBlazor.Services;
using AuthenticationHandler = Microscope.Boilerplate.Clients.Web.Blazor.Extensions.AuthenticationHeaderHandler;

namespace Microscope.Boilerplate.Clients.BFF.Configurations;

public static class SSRConfiguration
{
    public static IServiceCollection AddSSRConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddInteractiveWebAssemblyComponents();
        
        services.AddTodoAppClient()
            .ConfigureHttpClient(client =>
                    client.BaseAddress = new Uri("http://localhost:5215/"),
                clientBuilder => clientBuilder.AddHttpMessageHandler<AuthenticationHandler>()
            );

        services.AddTransient<AuthenticationHandler>();

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
        
        // services.AddAuthentication().AddOpenIdConnect(configuration =>
        // {
        //     configuration.Authority = "http://localhost:8083/realms/microscope/";
        //     configuration.ClientId = "boilerplate";
        //     configuration.ResponseType = "code";
        //     configuration.RequireHttpsMetadata = false;
        // });
        
        services.AddOidcAuthentication(options =>
            {
                string tenant = "master";
        
                var configKey = $"OIDC:{tenant}";
                configuration.Bind(configKey, options.ProviderOptions);
        
                options.ProviderOptions.ResponseType = "code";
                options.ProviderOptions.DefaultScopes.Add("roles");
                options.UserOptions.RoleClaim = "roles";
            })
            .AddAccountClaimsPrincipalFactory<CustomClaimsPrincipalFactory>();

        services
            .AddBlazoredLocalStorage()
            .AddLocalization(options => options.ResourcesPath = "Resources")
            .AddScoped<PreferenceService>()
            .AddSingleton<FeatureManagementService>();

        return services;
    }
}