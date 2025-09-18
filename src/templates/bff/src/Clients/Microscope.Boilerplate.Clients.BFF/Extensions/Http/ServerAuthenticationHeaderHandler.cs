namespace Microscope.Boilerplate.BFF.Extensions.Http;

public class ServerAuthenticationHeaderHandler(IHttpContextAccessor httpContextAccessor) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var httpContext = httpContextAccessor.HttpContext;

        if (httpContext is not null)
        {
            foreach (var cookie in httpContext.Request.Cookies)
            {
                request.Headers.Add("Cookie", $"{cookie.Key}={cookie.Value}");
            }
        }

        var response = await base.SendAsync(request, cancellationToken);

        var content = response.Content.ReadAsStringAsync();
        
        return response;
    }
}