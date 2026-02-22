using System.Net.Http.Json;
using Microscope.Boilerplate.Clients.SDK.GraphQL.Bff;
using Microsoft.AspNetCore.Components;
using StrawberryShake;

namespace Microscope.Boilerplate.Clients.Web.Shared.Services;

public class ClientFeatureManagementService(IBffClient client) : IFeatureManagementService
{
    private Dictionary<string, bool>? FeaturesFlags { get; set; }

    public async Task<Dictionary<string, bool>?> GetFeatureManagement()
    {
        if (FeaturesFlags is not null)
            return FeaturesFlags;

        var result = await client.GetFeatureFlags.ExecuteAsync();
        if (result.IsSuccessResult())
        {
            FeaturesFlags = result.Data?.Flags.ToDictionary(x => x.Key, x => x.Value);
        }
        
        return FeaturesFlags;
    }
}