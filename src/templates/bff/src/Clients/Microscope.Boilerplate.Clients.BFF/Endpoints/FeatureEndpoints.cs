using Microscope.Boilerplate.Clients.Web.Shared.Services;

namespace Microscope.Boilerplate.Clients.BFF.Endpoints;

public static class FeatureManagementEndpoints
{
    public static void MapFeatureManagementEndpoints(this WebApplication app)
    {
        app.MapGet("/api/features", FeatureFlags);
    }

    private static async Task<Dictionary<string, bool>?> FeatureFlags(IFeatureManagementService featureManagementService)
    {
        return await featureManagementService.GetFeatureManagement();
    }
}