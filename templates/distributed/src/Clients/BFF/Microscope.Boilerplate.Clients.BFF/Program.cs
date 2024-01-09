using Microscope.Boilerplate.Clients.BFF.Configurations;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Host = Microscope.Boilerplate.Clients.Web.Blazor.Host;

var builder = WebApplication.CreateBuilder(args);

// Open Telemetry
builder.Services.AddTelemetryConfiguration();
builder.Services.AddHealthCheckConfiguration();

// register reverse proxy configuration
builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddGraphQlGateway(builder.Configuration);

var SSREnabled = builder.Configuration.GetValue<bool>("SSREnabled");
if (SSREnabled)
{
    builder.Services.AddSSRConfiguration(builder.Configuration);
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}

app.UseHttpsRedirection();
app.UseHsts();

// expose reverse proxy
app.MapReverseProxy();

// expose grapqhql gateway
app.MapGraphQL();

app.UseStaticFiles();

// expose blazor app with SSR
if (SSREnabled)
{
    app.UseAntiforgery();
    app.MapRazorComponents<Host>()
        .AddInteractiveServerRenderMode()
        .AddInteractiveWebAssemblyRenderMode();
}
else // or as PWA
{
    app.UseBlazorFrameworkFiles();
    app.MapFallbackToFile("index.html");
}

app.MapHealthChecks("/health");
app.MapHealthChecks("/alive", new HealthCheckOptions
{
    Predicate = r => r.Tags.Contains("live")
});

app.Run();
