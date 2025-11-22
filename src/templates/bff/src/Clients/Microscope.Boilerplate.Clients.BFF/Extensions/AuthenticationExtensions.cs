using System.Security.Claims;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace Microscope.Boilerplate.Clients.BFF.Extensions;

public static class AuthenticationExtensions
{
    /// <summary>
    /// Register & validate options from configuration 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    private static IServiceCollection ValidateAuthenticationConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOptions<OidcAuthenticationOptions>()
            .Bind(configuration.GetSection(OidcAuthenticationOptions.ConfigurationKey))
            .Validate(x => new OidcAuthenticationOptionsValidator().Validate(x).IsValid)
            .ValidateOnStart();

        return services;
    }

    /// <summary>
    /// Add service authentication
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddAuthenticationConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.ValidateAuthenticationConfiguration(configuration);

        var oidcAuthenticationOptions = services
            .BuildServiceProvider()
            .GetRequiredService<IOptions<OidcAuthenticationOptions>>()
            .Value;

        switch (oidcAuthenticationOptions.Provider)
        {
            case OidcAuthenticationOptions.OIDC_PROVIDER:
                services.AddOidcAuthenticationConfiguration(oidcAuthenticationOptions);
                break;
            case OidcAuthenticationOptions.AZUREAD_PROVIDER:
                services.AddAzureAdAuthenticationConfiguration(oidcAuthenticationOptions);
                break;
        }

        services.ConfigureCookieOidcRefresh(CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme);
        services.AddAuthorization();

        return services;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="oidcAuthenticationOptions"></param>
    /// <returns></returns>
    private static IServiceCollection AddOidcAuthenticationConfiguration(this IServiceCollection services,
        OidcAuthenticationOptions oidcAuthenticationOptions)
    {
        services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
            {
                options.Authority = oidcAuthenticationOptions.Authority;
                options.ClientId = oidcAuthenticationOptions.ClientId;
                options.ClientSecret = oidcAuthenticationOptions.ClientSecret;

                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.ResponseType = OpenIdConnectResponseType.Code;

                options.RequireHttpsMetadata = false;
                options.SaveTokens = true;
                options.AuthenticationMethod = OpenIdConnectRedirectBehavior.RedirectGet;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = oidcAuthenticationOptions.NameClaimType,
                    RoleClaimType = oidcAuthenticationOptions.RoleClaimType,
                    ValidateIssuer = false
                };

                options.Events.OnRedirectToIdentityProvider = context =>
                {
                    if (!string.IsNullOrEmpty(oidcAuthenticationOptions.IssuerAddress))
                    {
                        // Intercept the redirection so the browser navigates to the right URL in your host
                        context.ProtocolMessage.IssuerAddress = oidcAuthenticationOptions.IssuerAddress;
                    }

                    return Task.CompletedTask;
                };

                options.Events.OnRedirectToIdentityProviderForSignOut = context =>
                {
                    if (!string.IsNullOrEmpty(oidcAuthenticationOptions.IssuerLogoutAddress))
                    {
                        // Intercept the redirection so the browser navigates to the right URL in your host
                        context.ProtocolMessage.IssuerAddress = oidcAuthenticationOptions.IssuerLogoutAddress;
                    }

                    return Task.CompletedTask;
                };

                foreach (var item in oidcAuthenticationOptions.Scopes)
                {
                    options.Scope.Add(item);
                }
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);
        
        return services;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="oidcAuthenticationOptions"></param>
    /// <returns></returns>
    private static IServiceCollection AddAzureAdAuthenticationConfiguration(this IServiceCollection services,
        OidcAuthenticationOptions oidcAuthenticationOptions)
    {
        services
            .AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddMicrosoftIdentityWebApp(options =>
            {
                options.Instance = oidcAuthenticationOptions.Authority;
                options.CallbackPath = oidcAuthenticationOptions.CallbackPath;
                options.ClientId = oidcAuthenticationOptions.ClientId;
                options.ClientSecret = oidcAuthenticationOptions.ClientSecret;
                options.TenantId = oidcAuthenticationOptions.TenantId;
                
                foreach (var scope in oidcAuthenticationOptions.Scopes)
                {
                    options.Scope.Add(scope);
                }
            });
        
        return services;
    }
}

public class OidcAuthenticationOptions
{
    public const string ConfigurationKey = "OIDC";
    public string Provider { get; set; } = OIDC_PROVIDER;

    public const string OIDC_PROVIDER = "OIDC";
    public const string AZUREAD_PROVIDER = "AzureAd";

    public string Authority { get; set; } // MSAL "Instance" 
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string TenantId { get; set; } // MSAL
    public string CallbackPath { get; set; } = "/signin-oidc"; // MSAL
    public string? NameClaimType { get; set; } = ClaimTypes.Name;
    public string? RoleClaimType { get; set; } = ClaimTypes.Role;
    public string? IssuerAddress { get; set; }
    public string? IssuerLogoutAddress { get; set; }
    public IEnumerable<string> Scopes { get; set; }
}

public class OidcAuthenticationOptionsValidator : AbstractValidator<OidcAuthenticationOptions>
{
    public OidcAuthenticationOptionsValidator()
    {
        RuleFor(x => x.Authority)
            .NotNull()
            .NotEmpty()
            .WithMessage("OIDC option Authority must have a value");

        RuleFor(x => x.ClientId)
            .NotNull()
            .NotEmpty()
            .WithMessage("OIDC option ClientId must have a value");

        RuleFor(x => x.ClientSecret)
            .NotNull()
            .NotEmpty()
            .WithMessage("OIDC option ClientSecret must have a value");

        When(x => x.Provider == OidcAuthenticationOptions.AZUREAD_PROVIDER, () =>
        {
            RuleFor(x => x.TenantId)
                .NotNull()
                .NotEmpty()
                .WithMessage("OIDC option TenantId must have a value when using AzureAD provider");
        });
    }
}