using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using ModelContextProtocol.Server;

namespace Microscope.Boilerplate.Todo.Slices.Features.ToggleTodoItem;

[McpServerToolType]
public static class ToggleTodoItemMcpTool
{
    [Authorize]
    [McpServerTool, Description("Toggle completion state of a todo item")]
    public static async Task<bool> ToggleTodoItem(
        IMediator mediator,
        [Description("Todo list id")] Guid todoListId,
        [Description("Todo item id")] Guid todoItemId)
    {
        return await mediator.Send(new ToggleTodoItemCommand(todoListId, todoItemId));
    }
}
