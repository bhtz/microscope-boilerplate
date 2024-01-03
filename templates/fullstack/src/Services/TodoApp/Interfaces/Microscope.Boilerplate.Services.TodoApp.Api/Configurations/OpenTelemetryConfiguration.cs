using FluentValidation;
using Microsoft.Extensions.Options;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Microscope.Boilerplate.Services.TodoApp.Api.Configurations;

public static class OpenTelemetryConfiguration
{
    public static IServiceCollection AddTelemetryConfiguration(this IServiceCollection services)
    {
        var option = services
            .BuildServiceProvider()
            .GetRequiredService<IOptions<OTELOptions>>()
            .Value;

        services.AddOpenTelemetry()
            .WithTracing(b => 
                b
                    .AddSource(option.ServiceName)
                    .ConfigureResource(resource => resource
                        .AddService(option.ServiceName))
                    .AddAspNetCoreInstrumentation()
                    .AddHotChocolateInstrumentation()
                    .AddConsoleExporter()
                    .AddOtlpExporter(o => o.Endpoint = new Uri(option.OtelExporterEndpoint)));
        
        services.AddLogging(l => l.AddOpenTelemetry(o =>
        {
            o.IncludeFormattedMessage = true;
            o.IncludeScopes = true;
            o.ParseStateValues = true;
            o.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(option.ServiceName));
        }));
        
        return services;
    }
}

public class OTELOptions
{
    public const string ConfigurationKey = "OTEL";

    public string ServiceName { get; set; } = "TodoApp.Api";
    public string OtelExporterEndpoint { get; set; }
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
