using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Microscope.Boilerplate.Services.TodoApp.Api.Configurations;

public static class OpenTelemetryConfiguration
{
    public static IServiceCollection AddTelemetryConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var serviceName = "TodoApp.Api";
        
        services.AddOpenTelemetry()
            .WithTracing(b => 
                b
                    .AddSource(serviceName)
                    .ConfigureResource(resource => resource
                        .AddService(serviceName))
                    .AddAspNetCoreInstrumentation()
                    .AddHotChocolateInstrumentation()
                    .AddConsoleExporter()
                    .AddOtlpExporter(o => o.Endpoint = new Uri("http://localhost:4317")));
        
        services.AddLogging(l => l.AddOpenTelemetry(o =>
        {
            o.IncludeFormattedMessage = true;
            o.IncludeScopes = true;
            o.ParseStateValues = true;
            o.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName));
        }));
        return services;
    }
}
