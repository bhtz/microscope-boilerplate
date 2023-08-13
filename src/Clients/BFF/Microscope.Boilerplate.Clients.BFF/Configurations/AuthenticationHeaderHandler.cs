using System.Net.Http.Headers;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Microscope.Boilerplate.Clients.BFF.Configurations;

public class AuthenticationHeaderHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public AuthenticationHeaderHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.Request.Headers.ContainsKey("Authorization"))
        {
            request.Headers.Authorization =
                AuthenticationHeaderValue.Parse(_httpContextAccessor.HttpContext.Request.Headers.Authorization);
        }

        var response = await base.SendAsync(request, cancellationToken);

        return response;
    }
}