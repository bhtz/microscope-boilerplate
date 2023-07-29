using HotChocolate.Authorization;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.FeatureManagement;

namespace Microscope.Boilerplate.Services.TodoApp.Api.GraphQL.Queries;

[Authorize]
public partial class Query
{
    [AllowAnonymous]
    public async Task<string> GetHealthCheck([Service] HealthCheckService healthCheck)
    {
        var result = await healthCheck.CheckHealthAsync();
        return result.Status.ToString();
    }
    
    [AllowAnonymous]
    public async Task<Dictionary<string, bool>> GetFeatures([Service] IFeatureManager featureManager)
    {
        return await featureManager.GetFeatureNamesAsync()
            .ToDictionaryAsync(
                feature => feature, 
                feature => featureManager.IsEnabledAsync(feature).GetAwaiter().GetResult());
    }
}
