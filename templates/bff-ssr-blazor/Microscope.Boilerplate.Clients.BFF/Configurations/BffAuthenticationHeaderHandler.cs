using System.Net.Http.Headers;

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
        if (_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.Request.Headers.ContainsKey("Authorization"))
        {
            request.Headers.Authorization =
                AuthenticationHeaderValue.Parse(_httpContextAccessor.HttpContext.Request.Headers.Authorization);
        }

        // foreach (var header in _httpContextAccessor.HttpContext?.Request.Headers)
        // {
        //     request.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
        // }

        var response = await base.SendAsync(request, cancellationToken);

        return response;
    }
}