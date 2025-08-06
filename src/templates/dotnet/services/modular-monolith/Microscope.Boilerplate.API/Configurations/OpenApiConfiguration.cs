namespace Microscope.Boilerplate.API.Configurations;

public static class OpenApiConfiguration
{
    public static IServiceCollection AddOpenApiConfiguration(this IServiceCollection services)
    {
        services.AddOpenApi();
        return services;
    }
}
