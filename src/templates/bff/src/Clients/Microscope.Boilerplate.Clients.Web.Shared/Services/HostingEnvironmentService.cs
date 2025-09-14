using Microsoft.JSInterop;

namespace Microscope.Boilerplate.Clients.Web.Shared.Services;

public class HostingEnvironmentService(IJSRuntime jsRuntime)
{
    public bool IsWebAssembly { get; set; } = jsRuntime is IJSInProcessRuntime;

    public string EnvironmentName => IsWebAssembly ? "WebAssembly" : "Server";
}
