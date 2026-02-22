using HotChocolate.Authorization;
using Microscope.Boilerplate.Clients.Web.Shared.Services;

namespace Microscope.Boilerplate.Todo.Slices.Features.FeatureManagement;

[QueryType]
public static class GetFeatureManagementResolver
{
    [AllowAnonymous]
    public static async Task<IDictionary<string, bool>?> GetFlags(IFeatureManagementService featureManagementService)
    {
        return await featureManagementService.GetFeatureManagement();
    }
}