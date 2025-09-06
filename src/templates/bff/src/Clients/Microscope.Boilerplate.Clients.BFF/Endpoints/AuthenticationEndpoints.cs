using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Microscope.Boilerplate.Clients.BFF.Endpoints;

public static class AuthenticationEndpoints
{
    public static void MapAuthenticationEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("auth");
        group.MapGet("/login", Login).AllowAnonymous();
        group.MapGet("/logout", Logout).RequireAuthorization();
    }

    private static ChallengeHttpResult Login(string? returnUrl)
    {
        var url = string.IsNullOrEmpty(returnUrl) ? "/" : returnUrl;
        return TypedResults.Challenge(new AuthenticationProperties() { RedirectUri = url },
            [CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme]);
    }

    private static Task<SignOutHttpResult> Logout(IHttpContextAccessor httpContextAccessor)
    {
        return Task.FromResult(TypedResults.SignOut(new AuthenticationProperties() { RedirectUri = "/" },
            [CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme]));
    }
}