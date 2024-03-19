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
        var option = services
            .BuildServiceProvider()
            .GetRequiredService<IOptions<OTELOptions>>()
            .Value;

        services.AddLogging(l => l.AddOpenTelemetry(o =>
        {
            o.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(option.ServiceName));
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
                    //.AddOtlpExporter(o => o.Endpoint = new Uri(option.OtelExporterEndpoint));
            })
            .WithTracing(builder =>
                builder
                    .AddAspNetCoreInstrumentation()
                    .AddHotChocolateInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddConsoleExporter()
                     .AddOtlpExporter());
                    //.AddOtlpExporter(o => o.Endpoint = new Uri(option.OtelExporterEndpoint)));
                    
        return services;
    }
    
    private static MeterProviderBuilder AddBuiltInMeters(this MeterProviderBuilder meterProviderBuilder) =>
        meterProviderBuilder.AddMeter(
            "Microsoft.AspNetCore.Hosting",
            "Microsoft.AspNetCore.Server.Kestrel",
            "System.Net.Http");
}

public class OTELOptions
{
    public const string ConfigurationKey = "OTEL";

    public string? ServiceName { get; set; }
    public string? OtelExporterEndpoint { get; set; }
}

public class OTELOptionsValidator : AbstractValidator<OTELOptions>
{
    public OTELOptionsValidator()
    {
        RuleFor(x => x.ServiceName)
            .NotNull()
            .NotEmpty()
            .WithMessage("OTEL service must have a name");
        
        RuleFor(x => x.OtelExporterEndpoint)
            .NotNull()
            .NotEmpty()
            .Must(x => Uri.IsWellFormedUriString(x, UriKind.Absolute))
            .WithMessage("OTEL service must have an exporter endpoint with well formed URI");
    }
}
