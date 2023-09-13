using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using AspNetCore.Authentication.ApiKey;
using Microscope.Boilerplate.Services.TodoApp.Api.Services;
using Microscope.Boilerplate.Services.TodoApp.Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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

                return c.Response.WriteAsync(c.Exception.InnerException.Message);
            }
        };

        var tenants = configuration.GetSection("Tenants").Get<List<JWTTenantConfiguration>>();

        foreach (var tenant in tenants)
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

                o.TokenValidationParameters.ValidateIssuer = true;
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
        var masterKey = configuration.GetValue<string>("MasterKey");
        var masterKeyTenant = masterKey?.Split("_").FirstOrDefault();
        
        if (masterKeyTenant is not null)
        {
            services.AddAuthentication(ApiKeyDefaults.AuthenticationScheme).AddApiKeyInHeader(options =>
            {
                options.Realm = "TODOAPP";
                options.KeyName = "X-TODOAPP-MASTER-KEY";
                options.IgnoreAuthenticationIfAllowAnonymous = true;
                options.Events = new ApiKeyEvents
                {
                    OnValidateKey = async (context) =>
                    {
                        var isValid = context.ApiKey.Equals(masterKey);

                        if (isValid)
                        {
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

public class JWTTenantConfiguration
{
    public string Authority { get; set; }
    public string Audience { get; set; }
    public string RoleClaim { get; set; }
}
