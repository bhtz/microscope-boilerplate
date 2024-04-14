using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Microscope.Boilerplate.ServiceDefaults.Configurations;

public static class HealthCheckConfiguration
{
    public static IServiceCollection AddDefaultHealthCheckConfiguration(this IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy(), ["live"]);
        
        return services;
    }
}
