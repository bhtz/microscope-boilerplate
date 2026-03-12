using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using ModelContextProtocol.Server;

namespace Microscope.Boilerplate.Todo.Slices.Features.UpdateTodoList;

[McpServerToolType]
public static class UpdateTodoListMcpTool
{
    [Authorize]
    [McpServerTool, Description("Update an existing todo list name")]
    public static async Task<bool> UpdateTodoList(
        IMediator mediator,
        [Description("Todo list id")] Guid todoListId,
        [Description("New todo list name")] string name)
    {
        return await mediator.Send(new UpdateTodoListCommand(name, todoListId));
    }
}
