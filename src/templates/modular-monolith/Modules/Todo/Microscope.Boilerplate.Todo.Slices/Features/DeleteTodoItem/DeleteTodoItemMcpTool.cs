using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using ModelContextProtocol.Server;

namespace Microscope.Boilerplate.Todo.Slices.Features.DeleteTodoItem;

[McpServerToolType]
public static class DeleteTodoItemMcpTool
{
    [Authorize]
    [McpServerTool, Description("Delete a todo item from a todo list")]
    public static async Task<bool> DeleteTodoItem(
        IMediator mediator,
        [Description("Todo list id")] Guid todoListId,
        [Description("Todo item id")] Guid todoItemId)
    {
        return await mediator.Send(new DeleteTodoItemCommand(todoListId, todoItemId));
    }
}
