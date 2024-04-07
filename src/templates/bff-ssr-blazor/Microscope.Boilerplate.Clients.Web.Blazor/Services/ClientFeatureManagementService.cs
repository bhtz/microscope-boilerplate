using System.Net.Http.Json;

namespace Microscope.Boilerplate.Clients.Web.Blazor.Services;

public class ClientFeatureManagementService(HttpClient client) : IFeatureManagementService
{
    private Dictionary<string, bool>? FeaturesFlags { get; set; }
    
    public async Task<Dictionary<string, bool>?> GetFeatureManagement()
    {
        if (FeaturesFlags is not null)
            return FeaturesFlags;

        FeaturesFlags = await client.GetFromJsonAsync<Dictionary<string, bool>>("/api/features");
        return FeaturesFlags;
    }
}