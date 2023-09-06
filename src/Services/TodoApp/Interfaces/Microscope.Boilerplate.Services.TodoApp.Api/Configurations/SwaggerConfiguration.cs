using Microsoft.OpenApi.Models;

namespace Microscope.Boilerplate.Services.TodoApp.Api.Configurations;

public static class SwaggerConfiguration
{
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "TodoApp.Api", Version = "v1" });
        });
        
        return services;
    }
}
