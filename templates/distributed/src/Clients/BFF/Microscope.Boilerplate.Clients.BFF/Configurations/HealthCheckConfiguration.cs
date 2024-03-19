using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Microscope.Boilerplate.Clients.BFF.Configurations;

public static class HealthCheckConfiguration
{
    public static IServiceCollection AddCustomHealthCheckConfiguration(this IServiceCollection services)
    {
        return services;
    }
}
