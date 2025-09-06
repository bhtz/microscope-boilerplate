using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace Microscope.Boilerplate.Clients.Web.Blazor.Configurations;

public class ClientAuthenticationHeaderHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
        var response = await base.SendAsync(request, cancellationToken);
        return response;
    }
}