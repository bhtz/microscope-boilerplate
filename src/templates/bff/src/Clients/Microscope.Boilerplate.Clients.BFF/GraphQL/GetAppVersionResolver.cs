using HotChocolate.Authorization;

namespace Microscope.Boilerplate.BFF.GraphQL;

[QueryType]
public static class GetAppVersionResolver
{
    [AllowAnonymous]
    public static async Task<string> GetAppVersion()
    {
        return "1.0.0";
    }
}
