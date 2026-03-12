using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using ModelContextProtocol.Server;

namespace Microscope.Boilerplate.Todo.Slices.Features.CreateTodoList;

[McpServerToolType]
public static class CreateTodoListMcpTool
{
    [Authorize]
    [McpServerTool, Description("Create a new todo list")]
    public static async Task<Guid> CreateTodoList(
        IMediator mediator,
        [Description("Todo list name")] string name)
    {
        return await mediator.Send(new CreateTodoListCommand(name));
    }
}
