using Microscope.Boilerplate.Clients.BFF.Providers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Microscope.Boilerplate.Clients.BFF.Configurations;

public static class CookieOidcRefresherConfiguration
{
    public static IServiceCollection ConfigureCookieOidcRefresh(this IServiceCollection services, string cookieScheme, string oidcScheme)
    {
        services.AddSingleton<CookieOidcRefresher>();
        services.AddOptions<CookieAuthenticationOptions>(cookieScheme).Configure<CookieOidcRefresher>((cookieOptions, refresher) =>
        {
            cookieOptions.Events.OnValidatePrincipal = context => refresher.ValidateOrRefreshCookieAsync(context, oidcScheme);
        });
        services.AddOptions<OpenIdConnectOptions>(oidcScheme).Configure(oidcOptions =>
        {
            // Request a refresh_token.
            oidcOptions.Scope.Add(OpenIdConnectScope.OfflineAccess);
            // Store the refresh_token.
            oidcOptions.SaveTokens = true;
        });
        return services;
    }
}