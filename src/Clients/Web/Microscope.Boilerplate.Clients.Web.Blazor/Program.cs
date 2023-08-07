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

string apiAddress;
var baseAddressConfiguration = builder.Configuration.GetValue<string>("APIBaseAddress");

if (!String.IsNullOrEmpty(baseAddressConfiguration))
{
    if(baseAddressConfiguration.StartsWith("http"))
        apiAddress = baseAddressConfiguration;
    else
        apiAddress = builder.HostEnvironment.BaseAddress + baseAddressConfiguration;
    
    builder.Services.AddTodoAppClient()
        .ConfigureHttpClient(client =>
                client.BaseAddress = new Uri(apiAddress),
            clientBuilder => clientBuilder.AddHttpMessageHandler<AuthenticationHeaderHandler>()
        );
}

// var clientName = "Boilerplate.Api";
// builder.Services.AddHttpClient(clientName, client => client.BaseAddress = new Uri(apiAddress))
//     .AddHttpMessageHandler<AuthenticationHeaderHandler>();

builder.Services.AddTransient<AuthenticationHeaderHandler>();

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
    .AddSingleton<FeatureManagementService>();

var host = builder.Build();

var storageService = host.Services.GetRequiredService<PreferenceService>();
if (storageService is not null)
{
    CultureInfo culture;
    var preference = await storageService.GetPreference();
    
    if (preference != null)
        culture = new CultureInfo(preference.LanguageCode);
    else
        culture = new CultureInfo("en-US");

    CultureInfo.DefaultThreadCurrentCulture = culture;
    CultureInfo.DefaultThreadCurrentUICulture = culture;
}

await host.RunAsync();