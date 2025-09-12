using Microscope.Boilerplate.BFF.Configurations;
using Microscope.Boilerplate.BFF.Configurations.Http;
using Microscope.Boilerplate.Clients.BFF.Configurations;
using Microscope.Boilerplate.Clients.BFF.Endpoints;
#if (Gateway)
using Microscope.Boilerplate.Clients.SDK.GraphQL.Gateway.Serializers;
#endif
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
#if (Gateway)
builder.Services.AddGraphQlGatewayConfiguration(builder.Environment, builder.Configuration);
#endif
builder.Services.AddGraphQlConfiguration();

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

#if (Gateway)
builder.Services.AddSerializer<Float8Serializer>();
builder.Services.AddGatewayClient().ConfigureHttpClient(
        client => client.BaseAddress = new Uri(baseAddress + "gateway"),
        clientBuilder => clientBuilder.AddHttpMessageHandler<ServerAuthenticationHeaderHandler>());
#endif

builder.Services.AddBffClient().ConfigureHttpClient(
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
if (builder.Configuration.GetSection("ReverseProxy").Exists())
    app.MapReverseProxy();
#endif

#if (Aspire)
app.MapDefaultEndpoints();
#endif

#if (Gateway)
app.MapGraphQL("/gateway", "gateway");
#endif

app.MapGraphQL("/graphql", "bff");

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

app.UseStatusCodePages(async context =>
{
    if (context.HttpContext.Response.StatusCode == 404)
    {
        context.HttpContext.Response.Redirect("/not-found");
    }
});

#endregion

app.Run();
