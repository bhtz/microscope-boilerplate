namespace Microscope.Boilerplate.Services.TodoApp.Api.Configurations;

public static class HostConfiguration
{
    public static IServiceCollection AddWebSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<SwaggerOptions>()
            .Bind(configuration.GetSection(SwaggerOptions.ConfigurationKey))
            .Validate(x => new SwaggerOptionsValidator().Validate(x).IsValid)
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
            .AddTelemetryConfiguration(configuration)
            .AddFeatureManagementConfiguration(configuration)
            .AddControllers();
        
        return services;
    }
}
