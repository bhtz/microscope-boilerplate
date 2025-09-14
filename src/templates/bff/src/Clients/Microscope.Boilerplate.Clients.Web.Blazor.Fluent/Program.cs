using System.Globalization;
using Microscope.Boilerplate.Clients.SDK.GraphQL.Gateway.Serializers;
using Microscope.Boilerplate.Clients.Web.Shared.Configurations;
using Microscope.Boilerplate.Clients.Web.Shared.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Localization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddFluentUiConfiguration();

var baseAddress = builder.HostEnvironment.BaseAddress;
builder.Services.AddScoped<ClientAuthenticationHeaderHandler>();

#if (Gateway)
builder.Services.AddSerializer<Float8Serializer>();
builder.Services.AddGatewayClient().ConfigureHttpClient(
    client => client.BaseAddress = new Uri(baseAddress + "gateway"),
    clientBuilder => clientBuilder.AddHttpMessageHandler<ClientAuthenticationHeaderHandler>());
#endif

builder.Services.AddBffClient().ConfigureHttpClient(
    client => client.BaseAddress = new Uri(baseAddress + "graphql"),
    clientBuilder => clientBuilder.AddHttpMessageHandler<ClientAuthenticationHeaderHandler>());

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthenticationStateDeserialization();

// builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddScoped<CookieService>();
builder.Services.AddSingleton<IFeatureManagementService, ClientFeatureManagementService>();

var host = builder.Build();

var cookieService = host.Services.GetRequiredService<CookieService>();
var cultureCookie = await cookieService.GetCultureFromCookie();

if (!string.IsNullOrEmpty(cultureCookie))
{
    var culture = CookieRequestCultureProvider.ParseCookieValue(cultureCookie)?.Cultures.FirstOrDefault().ToString();
    if (culture is not null)
    {
        var cultureInfo = new CultureInfo(culture);
        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
    }
}

await host.RunAsync();