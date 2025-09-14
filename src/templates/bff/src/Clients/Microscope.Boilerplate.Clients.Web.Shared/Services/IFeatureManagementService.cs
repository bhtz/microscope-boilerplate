namespace Microscope.Boilerplate.Clients.Web.Blazor.Services;

public interface IFeatureManagementService
{
    public Task<Dictionary<string, bool>?> GetFeatureManagement();
}