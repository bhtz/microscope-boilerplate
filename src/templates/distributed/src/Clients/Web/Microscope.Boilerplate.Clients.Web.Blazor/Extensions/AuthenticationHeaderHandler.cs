using System.Net.Http.Headers;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Microscope.Boilerplate.Clients.Web.Blazor.Extensions;

public class AuthenticationHeaderHandler : DelegatingHandler
{
    private readonly IAccessTokenProvider _accessTokenProvider;
    private readonly NavigationManager _navigationManager;
    private readonly ILocalStorageService _localStorageService;

    public AuthenticationHeaderHandler(IAccessTokenProvider accessTokenProvider, NavigationManager navigationManager, ILocalStorageService localStorageService)
    {
        _accessTokenProvider = accessTokenProvider;
        _navigationManager = navigationManager;
        _localStorageService = localStorageService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        if (request.Headers.Authorization?.Scheme != "Bearer")
        {
            var accessToken = string.Empty;
            var accessTokenResult = await _accessTokenProvider.RequestAccessToken();
            
            if (accessTokenResult.TryGetToken(out var token))
            { 
                accessToken = token.Value;
            }
            else
            {
                accessToken = await _localStorageService.GetItemAsync<string>("authtoken");
            }
            
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            _navigationManager.NavigateTo("/authentication/login", forceLoad: true);
        }

        return response;
    }
}