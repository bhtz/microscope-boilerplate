using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Microscope.Boilerplate.Clients.BFF.Configurations;

public static class HealthCheckConfiguration
{
    public static IServiceCollection AddHealthCheckConfiguration(this IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy(), ["live"]);
  
        return services;
    }
}
