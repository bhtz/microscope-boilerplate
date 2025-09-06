using Microsoft.AspNetCore.Authentication;

namespace Microscope.Boilerplate.BFF.Configurations.Http;

public class GatewayAuthenticationHeaderHandler(IHttpContextAccessor httpContextAccessor) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var httpContext = httpContextAccessor.HttpContext ??
                          throw new InvalidOperationException("No HttpContext available from the IHttpContextAccessor!");

        if (httpContext.User.Identity != null && httpContext.User.Identity.IsAuthenticated)
        {
            var accessToken = await httpContext.GetTokenAsync("access_token") ??
                              throw new InvalidOperationException("No access_token was saved");
            
            request.Headers.Authorization = new ("Bearer", accessToken);
        }
        
        // Client header forwarding ?
        // foreach (var header in _httpContextAccessor.HttpContext?.Request.Headers)
        // {
        //     request.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
        // }
        
        var response = await base.SendAsync(request, cancellationToken);
        
        return response;
    }
}