using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Microscope.Boilerplate.ServiceDefaults.Configurations;

public static class OpenTelemetryConfiguration
{
    public static IServiceCollection AddTelemetryConfiguration(this IServiceCollection services)
    {
        services.AddLogging(l => l.AddOpenTelemetry(o =>
        {
            o.IncludeFormattedMessage = true;
            o.IncludeScopes = true;
        }));

        services.Configure<OpenTelemetryLoggerOptions>(logging => logging.AddOtlpExporter());

        services.AddOpenTelemetry()
            .WithMetrics(metrics =>
            {
                metrics
                    .AddRuntimeInstrumentation(null)
                    .AddBuiltInMeters()
                     .AddOtlpExporter();
            })
            .WithTracing(builder =>
                builder
                    .AddAspNetCoreInstrumentation()
                    .AddHotChocolateInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddConsoleExporter()
                    .AddOtlpExporter());
                    
        return services;
    }
    
    private static MeterProviderBuilder AddBuiltInMeters(this MeterProviderBuilder meterProviderBuilder) =>
        meterProviderBuilder.AddMeter(
            "Microsoft.AspNetCore.Hosting",
            "Microsoft.AspNetCore.Server.Kestrel",
            "System.Net.Http");
}
