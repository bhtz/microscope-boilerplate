using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication;

namespace Microscope.Boilerplate.Clients.BFF.Configurations;

public class BffAuthenticationHeaderHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public BffAuthenticationHeaderHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (_httpContextAccessor.HttpContext != null)
        {
            var token = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
            if (token is not null)
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        // foreach (var header in _httpContextAccessor.HttpContext?.Request.Headers)
        // {
        //     request.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
        // }

        var response = await base.SendAsync(request, cancellationToken);

        return response;
    }
}