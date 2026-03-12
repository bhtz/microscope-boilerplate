using Microscope.Boilerplate.Clients.Web.Shared.Services;
using Microsoft.AspNetCore.Authorization;

namespace Microscope.Boilerplate.BFF.GraphQL;

[QueryType]
public static class GetFeatureManagementResolver
{
    [AllowAnonymous]
    public static async Task<IDictionary<string, bool>?> GetFlags(IFeatureManagementService featureManagementService)
    {
        return await featureManagementService.GetFeatureManagement();
    }
}