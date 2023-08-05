using System.Net.Http.Json;
using Microscope.Boilerplate.Clients.SDK.GraphQL;
using StrawberryShake;

namespace Microscope.Boilerplate.Clients.Web.Blazor.Services;

public class FeatureManagementService
{
    private readonly ITodoAppClient _client;
    private IEnumerable<IGetFeatures_Features> Features { get; set; }
    
    public FeatureManagementService(ITodoAppClient client)
    {
        _client = client;
    }
    
    public async Task LoadFeatureManagement()
    {
        var featuresResult = await _client.GetFeatures.ExecuteAsync();
        if (featuresResult.IsSuccessResult())
        {
            Features = featuresResult.Data.Features;
        }
    }
}