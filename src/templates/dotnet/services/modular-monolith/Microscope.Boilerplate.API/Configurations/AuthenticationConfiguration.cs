using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using AspNetCore.Authentication.ApiKey;
using FluentValidation;
using Microscope.Boilerplate.API.Services;
using Microscope.Framework.Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace Microscope.Boilerplate.API.Configurations;

public static class AuthenticationConfiguration
{
    /// <summary>
    /// Add service authentication
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddJwtAuthenticationConfiguration(configuration)
            .AddApiKeyAuthenticationConfiguration(configuration);
        
        services.AddHttpContextAccessor();
        services.AddScoped<IIdentityService, IdentityService>();
        
        return services;
    }
    
    private static IServiceCollection ValidateJwtAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<OidcAuthenticationOptions>()
            .Bind(configuration.GetSection(OidcAuthenticationOptions.ConfigurationKey))
            .Validate(x => new OidcAuthenticationOptionsValidator().Validate(x).IsValid)
            .ValidateOnStart();

        return services;
    }
    
    private static IServiceCollection ValidateApiKeyAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<ApiKeyAuthenticationOptions>()
            .Bind(configuration.GetSection(ApiKeyAuthenticationOptions.ConfigurationKey))
            .Validate(x => new ApiKeyAuthenticationOptionsValidator().Validate(x).IsValid)
            .ValidateOnStart();

        return services;
    }
    
    private static IServiceCollection AddJwtAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.ValidateJwtAuthenticationConfiguration(configuration);

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
    
    private  static IServiceCollection AddApiKeyAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.ValidateApiKeyAuthenticationConfiguration(configuration);
        
        var option = services
            .BuildServiceProvider()
            .GetRequiredService<IOptions<ApiKeyAuthenticationOptions>>()
            .Value;
        
        var masterKeyTenant = option.MasterKey.Split("_").FirstOrDefault();
        
        if (masterKeyTenant is not null)
        {
            services
                .AddAuthentication(ApiKeyDefaults.AuthenticationScheme)
                .AddApiKeyInHeader(options =>
            {
                options.Realm = option.Realm;
                options.KeyName = option.KeyName;
                options.IgnoreAuthenticationIfAllowAnonymous = true;
                options.Events = new ApiKeyEvents
                {
                    OnValidateKey = async (context) =>
                    {
                        var isValid = context.ApiKey.Equals(option.MasterKey);

                        if (isValid)
                        {
                            var claims = new[]
                            {
                                new Claim(ClaimTypes.NameIdentifier, Guid.Empty.ToString(), ClaimValueTypes.String, context.Options.ClaimsIssuer),
                                new Claim(ClaimTypes.Name, option.MasterName, ClaimValueTypes.String, context.Options.ClaimsIssuer),
                                new Claim(ClaimTypes.Email, option.MasterMail),
                                new Claim(ClaimTypes.Role, option.MasterRole),
                                new Claim("iss", masterKeyTenant)
                            };
                            context.Principal = new ClaimsPrincipal(new ClaimsIdentity(claims, context.Scheme.Name));
                            context.Success();
                        }
                        else
                        {
                            context.NoResult();
                        }
                    }
                };
            });
        }

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

public class ApiKeyAuthenticationOptions
{
    public const string ConfigurationKey = "ApiKey";
    
    public string Realm { get; set; }
    public string KeyName { get; set; }
    public string MasterKey { get; set; }
    public string MasterName { get; set; } = "Admin";
    public string MasterRole { get; set; } = "administrator";
    public string MasterMail { get; set; } = "admin@microscope.net";
}

public class ApiKeyAuthenticationOptionsValidator : AbstractValidator<ApiKeyAuthenticationOptions>
{
    public ApiKeyAuthenticationOptionsValidator()
    {
        RuleFor(x => x.MasterKey)
            .NotNull()
            .NotEmpty()
            .Must(x => x.Contains('_'))
            .WithMessage("Tenant option authority must have a value & contain '_' char");
        
        RuleFor(x => x.Realm)
            .NotNull()
            .NotEmpty()
            .WithMessage("API Key option realm must have a value");
        
        RuleFor(x => x.MasterMail)
            .NotNull()
            .NotEmpty()
            .EmailAddress()
            .WithMessage("API Key option master mail must have be a valid email address");
    }
}