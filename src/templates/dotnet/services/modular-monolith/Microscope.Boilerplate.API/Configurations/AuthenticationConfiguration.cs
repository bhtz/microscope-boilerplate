using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using FluentValidation;
using Microscope.Boilerplate.API.Services;
using Microscope.Framework.Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace Microscope.Boilerplate.API.Configurations;

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

        var authenticationBuilder = services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(option =>
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
                
            option.Authority = oidcAuthenticationOptions.Authority;
            option.Audience = oidcAuthenticationOptions.ClientId;
            
            if (!string.IsNullOrEmpty(oidcAuthenticationOptions.RoleClaimType))
            {
                option.TokenValidationParameters.RoleClaimType = oidcAuthenticationOptions.RoleClaimType;
            }

            option.TokenValidationParameters.ValidateIssuer = false;
            option.TokenValidationParameters.ValidateAudience = true;
            option.RequireHttpsMetadata = false;
            
            option.Events = new JwtBearerEvents()
            {
                OnAuthenticationFailed = c =>
                {
                    c.NoResult();
                    c.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    c.Response.ContentType = "text/plain";

                    return c.Response.WriteAsync(c.Exception.Message);
                }
            };
        });

        services.AddHttpContextAccessor();
        services.AddScoped<IIdentityService, IdentityService>();

        return services;
    }
}

public class OidcAuthenticationOptions
{
    public const string ConfigurationKey = "OIDC";

    public string Authority { get; set; }
    public string ClientId { get; set; }
    
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
    }
}