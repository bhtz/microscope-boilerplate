namespace Microscope.Boilerplate.Services.TodoApp.Api.Configurations;

public static class HostConfiguration
{
    public static IServiceCollection AddWebSettings(this IServiceCollection services, IConfiguration configuration)
    {
        // TODO CHECK IF REQUIRED
        services.AddServiceDiscovery();
        
        services.AddOptions<SwaggerOptions>()
            .Bind(configuration.GetSection(SwaggerOptions.ConfigurationKey))
            .Validate(x => new SwaggerOptionsValidator().Validate(x).IsValid)
            .ValidateOnStart();
        
        services.AddOptions<OTELOptions>()
            .Bind(configuration.GetSection(OTELOptions.ConfigurationKey))
            .Validate(x => new OTELOptionsValidator().Validate(x).IsValid)
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
        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddGraphQLConfiguration()
            .AddCorsConfiguration()
            .AddSwaggerConfiguration()
            .AddHealthCheckConfiguration()
            .AddHttpClient()
            .AddAuthorizationConfiguration()
            .AddJwtAuthenticationConfiguration(configuration)
            .AddApiKeyAuthenticationConfiguration(configuration)
            .AddTelemetryConfiguration()
            .AddFeatureManagementConfiguration(configuration)
            .AddControllers();
        
        return services;
    }
}
