using System.Security.Claims;
using FluentValidation;
using Microscope.Boilerplate.Clients.BFF.Providers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace Microscope.Boilerplate.Clients.BFF.Configurations;

public static class AuthenticationConfiguration
{
    /// <summary>
    /// Register & validate options from configuration 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    private static IServiceCollection ValidateAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
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
    public static IServiceCollection AddAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.ValidateAuthenticationConfiguration(configuration);
        
        var oidcAuthenticationOptions = services
            .BuildServiceProvider()
            .GetRequiredService<IOptions<OidcAuthenticationOptions>>()
            .Value;
        
        services.AddAuthorization();
        services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
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
            });
        
        services.AddHttpContextAccessor();
        
        return services;
    }
}

public class OidcAuthenticationOptions
{
    public const string ConfigurationKey = "OIDC";

    public string Authority { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
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
    }
}