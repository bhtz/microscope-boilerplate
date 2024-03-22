using Microscope.Boilerplate.ServiceDefaults;

namespace Microscope.Boilerplate.Services.TodoApp.Api.Configurations;

public static class HostConfiguration
{
    public static IServiceCollection AddWebSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<SwaggerOptions>()
            .Bind(configuration.GetSection(SwaggerOptions.ConfigurationKey))
            .Validate(x => new SwaggerOptionsValidator().Validate(x).IsValid)
            .ValidateOnStart();
        
        services.AddOptions<OIDCAuthenticationOptions>()
            .Bind(configuration.GetSection(OIDCAuthenticationOptions.ConfigurationKey))
            .Validate(x => new OIDCAuthenticationOptionsValidator().Validate(x).IsValid)
            .ValidateOnStart();
        
        services.AddOptions<ApiKeyAuthenticationOptions>()
            .Bind(configuration.GetSection(ApiKeyAuthenticationOptions.ConfigurationKey))
            .Validate(x => new ApiKeyAuthenticationOptionsValidator().Validate(x).IsValid)
            .ValidateOnStart();
        
        return services;
    }
    
    public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Register default services : service discovery, healthcheck, open telemetry
        services.AddServiceDefaults();
        
        // register custom services
        services
            .AddEndpointsApiExplorer()
            .AddGraphQLConfiguration()
            .AddCorsConfiguration()
            .AddSwaggerConfiguration()
            .AddCustomHealthCheckConfiguration()
            .AddHttpClient()
            .AddAuthorizationConfiguration()
            .AddJwtAuthenticationConfiguration()
            .AddApiKeyAuthenticationConfiguration()
            .AddFeatureManagementConfiguration(configuration) // TODO: to service default with custom
            .AddControllers();
        
        return services;
    }
}
