using System.Globalization;
using Microscope.Boilerplate.Clients.Web.Blazor.Configurations;
using Microscope.Boilerplate.Clients.Web.Blazor.Providers;
using Microscope.Boilerplate.Clients.Web.Blazor.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Localization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

var baseAddress = builder.HostEnvironment.BaseAddress;

builder.Services.AddScoped<HostingEnvironmentService>();
builder.Services.AddScoped<ClientAuthenticationHeaderHandler>();
builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(baseAddress) });
builder.Services.AddBFFClient().ConfigureHttpClient(
    client => client.BaseAddress = new Uri(baseAddress + "graphql"),
    clientBuilder => clientBuilder.AddHttpMessageHandler<ClientAuthenticationHeaderHandler>());

builder.Services.AddUiConfiguration();
builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources" );
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
