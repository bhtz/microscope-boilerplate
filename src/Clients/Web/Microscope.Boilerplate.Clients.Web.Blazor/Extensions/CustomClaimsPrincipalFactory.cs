using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;

namespace Microscope.Boilerplate.Clients.Web.Blazor.Extensions;

public class CustomClaimsPrincipalFactory : AccountClaimsPrincipalFactory<RemoteUserAccount>
{
    public CustomClaimsPrincipalFactory(IAccessTokenProviderAccessor accessor) : base(accessor)
    {
    }
    
    public async override ValueTask<ClaimsPrincipal> CreateUserAsync(RemoteUserAccount account, RemoteAuthenticationUserOptions options)
    {
        var user = await base.CreateUserAsync(account, options);

        if (user.Identity is not { IsAuthenticated: true }) return user ?? new ClaimsPrincipal();
        var claimsIdentity = (ClaimsIdentity)user.Identity;
        if (claimsIdentity is not null)
        {
            MapArrayClaimsToMultipleSeparateClaims(account, claimsIdentity);
        }

        return user ?? new ClaimsPrincipal();
    }
    
    private void MapArrayClaimsToMultipleSeparateClaims(RemoteUserAccount account, ClaimsIdentity claimsIdentity)
    {
        Console.WriteLine($"MapArrayClaimsToMultipleSeparateClaims: {account.AdditionalProperties.Count}");
        foreach (var prop in account.AdditionalProperties)
        {
            var key = prop.Key;
            var value = prop.Value;

            if (value != null && (value is JsonElement element && element.ValueKind == JsonValueKind.Array))
            {
                claimsIdentity.RemoveClaim(claimsIdentity.FindFirst(prop.Key));
                var claims = element.EnumerateArray()
                    .Select(x => new Claim(prop.Key, x.ToString()));
                claimsIdentity.AddClaims(claims);
            }
        }
    }
}