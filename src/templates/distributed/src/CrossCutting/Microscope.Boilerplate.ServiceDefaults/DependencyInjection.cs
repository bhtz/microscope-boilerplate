using Microscope.Boilerplate.ServiceDefaults.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Microscope.Boilerplate.ServiceDefaults;

public static class ServiceDefaults
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddServiceDefaults(this IServiceCollection services)
    {
        services.AddServiceDiscovery();
        services.AddDefaultHealthCheckConfiguration();
        services.AddTelemetryConfiguration();
        
        return services;
    }
    
    public static WebApplication MapDefaultEndpoints(this WebApplication app)
    {
        // Uncomment the following line to enable the Prometheus endpoint 
        // (requires the OpenTelemetry.Exporter.Prometheus.AspNetCore package)
        // app.MapPrometheusScrapingEndpoint();

        // Only health checks tagged with the "live" tag must pass for app 
        // to be considered alive
        app.MapHealthChecks("/health");
        app.MapHealthChecks("/alive", new HealthCheckOptions
        {
            Predicate = r => r.Tags.Contains("live")
        });

        return app;
    }
}