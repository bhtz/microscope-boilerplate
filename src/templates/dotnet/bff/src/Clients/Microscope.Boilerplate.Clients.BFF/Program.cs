using Microscope.Boilerplate.BFF.Configurations.Http;
using Microscope.Boilerplate.Clients.BFF.Configurations;
using Microscope.Boilerplate.Clients.BFF.Endpoints;
using Microscope.Boilerplate.Clients.Web.Blazor;
using Microscope.Boilerplate.Clients.Web.Blazor.Configurations;
#if (Aspire)
using Microscope.Boilerplate.ServiceDefaults;
#endif
using Host = Microscope.Boilerplate.Clients.BFF.Components.Pages.Host;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
#if (Aspire)
builder.AddServiceDefaults();
#endif

builder.Services.AddProblemDetails();

#region Services required for BFF API

// shared with server side rendering
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthenticationConfiguration(builder.Configuration);
builder.Services.AddFeatureManagementConfiguration(builder.Configuration);
#if (Yarp)
builder.Services.AddReverseProxyConfiguration(builder.Configuration);
#endif
builder.Services.AddGraphQlGatewayConfiguration(builder.Environment, builder.Configuration);

#endregion

// Add services to the container.
#region Services required for Blazor Server

builder.Services.AddCascadingAuthenticationState();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents()
    .AddAuthenticationStateSerialization(x => x.SerializeAllClaims = true); // share state between server and client

// required service for Server side rendering
// mirror the services from the client side
builder.Services.AddUiConfiguration();
var baseAddress = builder.Configuration.GetValue<string>("BaseAddress") ?? throw new InvalidOperationException("BaseAddress configuration cannot be null");
builder.Services.AddScoped<ServerAuthenticationHeaderHandler>();
builder.Services.AddBFFClient().ConfigureHttpClient(
        client => client.BaseAddress = new Uri(baseAddress + "graphql"),
        clientBuilder => clientBuilder.AddHttpMessageHandler<ServerAuthenticationHeaderHandler>());
builder.Services.AddLocalizationConfiguration(builder.Configuration);

#endregion

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

#if (Yarp)
// Map BFF endpoints
if (builder.Configuration.GetSection("ReverseProxy").Exists())
    app.MapReverseProxy();
#endif

#if (Aspire)
app.MapDefaultEndpoints();
#endif

app.MapGraphQL();
app.MapAuthenticationEndpoints();
app.MapFeatureManagementEndpoints();
app.MapCultureEndpoints();

# region Middeleware for Blazor Server

app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<Host>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(_Imports).Assembly);

#endregion

app.Run();
