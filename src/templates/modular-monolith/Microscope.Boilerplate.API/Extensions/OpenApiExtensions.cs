namespace Microscope.Boilerplate.API.Extensions;

public static class OpenApiExtensions
{
    public static IServiceCollection AddOpenApiConfiguration(this IServiceCollection services)
    {
        services.AddOpenApi();
        return services;
    }
}
