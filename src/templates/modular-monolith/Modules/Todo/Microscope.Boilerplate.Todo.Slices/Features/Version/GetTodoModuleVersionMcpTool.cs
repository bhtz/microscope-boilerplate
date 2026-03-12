using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using ModelContextProtocol.Server;

namespace Microscope.Boilerplate.Todo.Slices.Features.Version;

[McpServerToolType]
public static class GetTodoModuleVersionMcpTool
{
    [AllowAnonymous]
    [McpServerTool, Description("Get the Todo module version")]
    public static async Task<string> GetTodoModuleVersion(IMediator mediator)
    {
        return await mediator.Send(new GetTodoVersionQuery());
    }
}
