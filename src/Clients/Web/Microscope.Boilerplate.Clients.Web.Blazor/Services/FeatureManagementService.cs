using System.Net.Http.Json;

namespace Microscope.Boilerplate.Clients.Web.Blazor.Services;

public class FeatureManagementService
{
    public FeaturesResult Features { get; set; }
    
    private HttpClient _httpClient;
    
    public FeatureManagementService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task LoadFeatureManagement()
    {
        var res = await _httpClient.GetAsync("/api/Features");

        if (res.IsSuccessStatusCode)
        {
            Features = await res.Content.ReadFromJsonAsync<FeaturesResult>();
        }
    }
    
    public record FeaturesResult(bool IaGeneration);
}