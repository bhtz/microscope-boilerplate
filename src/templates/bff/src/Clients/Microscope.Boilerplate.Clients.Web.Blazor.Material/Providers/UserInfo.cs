using System.Security.Claims;

namespace Microscope.Boilerplate.Clients.Web.Blazor.Material.Providers;

// Add properties to this class and update the server and client AuthenticationStateProviders
// to expose more information about the authenticated user to the client.
public sealed class UserInfo
{
    public required Dictionary<string, string> Claims { get; init; }

    public static UserInfo FromClaimsPrincipal(ClaimsPrincipal principal) =>
        new()
        {
            Claims = principal.Claims.ToDictionary(x => x.Type, x => x.Value)
        };
}