namespace Microscope.Boilerplate.Clients.Web.Shared.Services;

public interface IFeatureManagementService
{
    public Task<Dictionary<string, bool>?> GetFeatureManagement();
}