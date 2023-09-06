namespace Microscope.Boilerplate.Services.TodoApp.Api.Configurations;

public static class CorsConfiguration
{
    public static IServiceCollection AddCorsConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(o => o.AddPolicy("allow-all", builder =>
        {
            builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
        }));

        return services;
    }
}
