using System.Globalization;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microscope.Boilerplate.Clients.Web.Blazor;
using Microscope.Boilerplate.Clients.Web.Blazor.Extensions;
using Microscope.Boilerplate.Clients.Web.Blazor.Services;
using MudBlazor;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

Uri apiAddress;
var clientName = "Boilerplate.Api";
var baseAddressConfiguration = builder.Configuration.GetValue<string>("APIBaseAddress");

if (String.IsNullOrEmpty(baseAddressConfiguration))
    apiAddress = new Uri(builder.HostEnvironment.BaseAddress);
else
    apiAddress = new Uri(baseAddressConfiguration);

builder.Services.AddTransient<AuthenticationHeaderHandler>();

builder.Services.AddHttpClient(clientName, client => client.BaseAddress = apiAddress)
    .AddHttpMessageHandler<AuthenticationHeaderHandler>();
 
builder.Services.AddTodoAppClient()
    .ConfigureHttpClient(client =>
        client.BaseAddress = apiAddress,
        clientBuilder => clientBuilder.AddHttpMessageHandler<AuthenticationHeaderHandler>()
    );

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 3000;
    config.SnackbarConfiguration.HideTransitionDuration = 100;
    config.SnackbarConfiguration.ShowTransitionDuration = 100;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Outlined;
});

builder.Services.AddOidcAuthentication(options =>
{
    string tenant = "master";
    
    // var subDomain = GetSubDomain(new Uri(builder.HostEnvironment.BaseAddress));
    // if (!string.IsNullOrEmpty(subDomain))
    //     tenant = subDomain.Split('.')[0];
    //     //tenant = subDomain;
        
    var configKey = $"OIDC:{tenant}";
    builder.Configuration.Bind(configKey, options.ProviderOptions);

    options.ProviderOptions.ResponseType = "code";
    options.ProviderOptions.DefaultScopes.Add("roles");
    options.UserOptions.RoleClaim = "roles";
})
.AddAccountClaimsPrincipalFactory<CustomClaimsPrincipalFactory>();

builder.Services
    .AddBlazoredLocalStorage()
    .AddLocalization(options => options.ResourcesPath = "Resources")
    .AddScoped<PreferenceService>()
    .AddScoped<FeatureManagementService>();

var host = builder.Build();

var storageService = host.Services.GetRequiredService<PreferenceService>();
if (storageService is not null)
{
    CultureInfo culture;
    var preference = await storageService.GetPreference();
    
    if (preference != null)
        culture = new CultureInfo(preference.LanguageCode);
    else
        culture = new CultureInfo("fr-FR");

    CultureInfo.DefaultThreadCurrentCulture = culture;
    CultureInfo.DefaultThreadCurrentUICulture = culture;
}

await host.RunAsync();