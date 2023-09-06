using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;

namespace Microscope.Boilerplate.Services.TodoApp.Api.Controllers;

public class FeaturesController : ControllerBase
{
    private readonly IFeatureManager _featureManager;

    public FeaturesController(IFeatureManager featureManager)
    {
        _featureManager = featureManager;
    }

    [HttpGet]
    [Route("api/[controller]")]
    public async Task<Dictionary<string, bool>> ListFeatures()
    {
        return await _featureManager.GetFeatureNamesAsync()
            .ToDictionaryAsync(
                feature => feature, 
                feature => _featureManager.IsEnabledAsync(feature).GetAwaiter().GetResult());
    }
}