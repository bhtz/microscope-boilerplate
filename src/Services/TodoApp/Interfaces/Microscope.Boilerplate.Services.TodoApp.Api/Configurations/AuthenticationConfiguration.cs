using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using AspNetCore.Authentication.ApiKey;
using FluentValidation;
using Microscope.Boilerplate.Services.TodoApp.Api.Services;
using Microscope.Boilerplate.Services.TodoApp.Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace Microscope.Boilerplate.Services.TodoApp.Api.Configurations;

public static class AuthenticationConfiguration
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddJwtAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var option = services
            .BuildServiceProvider()
            .GetRequiredService<IOptions<OIDCAuthenticationOptions>>()
            .Value;
        
        var authenticationBuilder = services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        });

        var JwtEvent = new JwtBearerEvents()
        {
            OnAuthenticationFailed = c =>
            {
                c.NoResult();
                c.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                c.Response.ContentType = "text/plain";

                return c.Response.WriteAsync(c.Exception.Message);
            }
        };
        
        foreach (var tenant in option.Tenants)
        {
            authenticationBuilder.AddJwtBearer(o =>
            {
                JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
                
                o.Authority = tenant.Authority;
                o.Audience = tenant.Audience;
                
                if (!string.IsNullOrEmpty(tenant.RoleClaim))
                {
                    o.TokenValidationParameters.RoleClaimType = tenant.RoleClaim;
                }

                o.TokenValidationParameters.ValidateIssuer = false;
                o.TokenValidationParameters.ValidateAudience = true;
                
                o.RequireHttpsMetadata = false;
                o.Events = JwtEvent;
            });
        }
        
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddTransient<IIdentityService, IdentityService>();

        return services;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddApiKeyAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var option = services
            .BuildServiceProvider()
            .GetRequiredService<IOptions<ApiKeyAuthenticationOptions>>()
            .Value;
        
        var masterKeyTenant = option.MasterKey.Split("_").FirstOrDefault();
        
        if (masterKeyTenant is not null)
        {
            services.AddAuthentication(ApiKeyDefaults.AuthenticationScheme).AddApiKeyInHeader(options =>
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
                            // TODO : Add Name, Email & Role in settings
                            var claims = new[]
                            {
                                new Claim(ClaimTypes.NameIdentifier, Guid.Empty.ToString(), ClaimValueTypes.String, context.Options.ClaimsIssuer),
                                new Claim(ClaimTypes.Name, "Admin", ClaimValueTypes.String, context.Options.ClaimsIssuer),
                                new Claim(ClaimTypes.Email, "admin@microscope.net"),
                                new Claim(ClaimTypes.Role, "administrator"),
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

public class JWTTenantOptions
{
    public string Authority { get; set; }
    public string Audience { get; set; }
    public string RoleClaim { get; set; } = "roles";
}

public class JWTTenantOptionsValidator : AbstractValidator<JWTTenantOptions>
{
    public JWTTenantOptionsValidator()
    {
        RuleFor(x => x.Authority)
            .NotNull()
            .NotEmpty()
            .WithMessage("Tenant option authority must have a value");
        
        RuleFor(x => x.Audience)
            .NotNull()
            .NotEmpty()
            .WithMessage("Tenant option audience must have a value");
    }
}

public class ApiKeyAuthenticationOptions
{
    public const string ConfigurationKey = "Auth:ApiKey";
    
    public string Realm { get; set; }
    public string KeyName { get; set; }
    public string MasterKey { get; set; }
}

public class ApiKeyAuthenticationOptionsValidator : AbstractValidator<ApiKeyAuthenticationOptions>
{
    public ApiKeyAuthenticationOptionsValidator()
    {
        RuleFor(x => x.MasterKey)
            .NotNull()
            .NotEmpty()
            .Must(x => x.Contains('_'))
            .WithMessage("Tenant option authority must have a value & contain '-' char");
        
        RuleFor(x => x.Realm)
            .NotNull()
            .NotEmpty()
            .WithMessage("API Key option realm must have a value");
    }
}

public class OIDCAuthenticationOptions
{
    public const string ConfigurationKey = "Auth:OIDC";

    public List<JWTTenantOptions> Tenants { get; set; }
}

public class OIDCAuthenticationOptionsValidator : AbstractValidator<OIDCAuthenticationOptions>
{
    public OIDCAuthenticationOptionsValidator()
    {
        RuleFor(x => x.Tenants)
            .NotNull()
            .NotEmpty()
            .WithMessage("OIDC option tenants must have a value");
    }
}
