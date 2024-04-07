using Microscope.Boilerplate.Clients.BFF.Configurations;
using Microscope.Boilerplate.Clients.BFF.Endpoints;
using Microscope.Boilerplate.Clients.BFF.Providers;
using Microscope.Boilerplate.Clients.Web.Blazor;
using Microscope.Boilerplate.Clients.Web.Blazor.Configurations;
using Microscope.Boilerplate.Clients.Web.Blazor.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
using Host = Microscope.Boilerplate.Clients.BFF.Components.Host;

var builder = WebApplication.CreateBuilder(args);

// BFF Configuration
builder.Services.AddScoped<BffAuthenticationHeaderHandler>();
builder.Services.AddAuthenticationConfiguration(builder.Configuration);
builder.Services.AddReverseProxyConfiguration(builder.Configuration);
builder.Services.AddGraphQlGatewayConfiguration(builder.Configuration);

var isWebAppEnabled = builder.Configuration.GetValue<bool>("EnableWebApp", true);

// Web application Configuration
if (isWebAppEnabled)
{
    var baseAddress = builder.Configuration.GetValue<string>("BaseAddress") ?? throw new InvalidOperationException("BaseAddress configuration cannot be null");

    builder.Services.AddFeatureManagementConfiguration(builder.Configuration);
    builder.Services.AddLocalizationConfiguration(builder.Configuration);
    builder.Services.AddScoped<ClientAuthenticationHeaderHandler>();
    builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();
    builder.Services.AddCascadingAuthenticationState();
    builder.Services.AddUiConfiguration();
    builder.Services.AddRazorComponents()
        .AddInteractiveServerComponents()
        .AddInteractiveWebAssemblyComponents();
    builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAddress) });
    builder.Services.AddBFFClient().ConfigureHttpClient(
        client => client.BaseAddress = new Uri(baseAddress + "graphql"),
        clientBuilder => clientBuilder.AddHttpMessageHandler<ClientAuthenticationHeaderHandler>());
}

var app = builder.Build();

string[] supportedCultures = ["en-US", "fr-FR"];
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// Map BFF endpoints
app.MapReverseProxy();
app.MapGraphQL();
app.MapAuthenticationEndpoints();
app.MapFeatureManagementEndpoints();
app.MapGet("/version", () => new { Version = "1.0.0" });

// Map SSR blazor web app
if (isWebAppEnabled)
{
    app.UseStaticFiles();
    app.UseAntiforgery();
    
    app.MapCultureEndpoints();
    app.MapRazorComponents<Host>()
        .AddInteractiveServerRenderMode()
        .AddInteractiveWebAssemblyRenderMode()
        .AddAdditionalAssemblies(typeof(_Imports).Assembly);
}

app.Run();
