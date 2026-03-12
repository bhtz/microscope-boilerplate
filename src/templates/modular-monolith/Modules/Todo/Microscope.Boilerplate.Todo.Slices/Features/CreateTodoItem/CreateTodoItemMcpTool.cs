using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using ModelContextProtocol.Server;

namespace Microscope.Boilerplate.Todo.Slices.Features.CreateTodoItem;

[McpServerToolType]
public static class CreateTodoItemMcpTool
{
    [Authorize]
    [McpServerTool, Description("Create a new todo item in a todo list")]
    public static async Task<Guid> CreateTodoItem(
        IMediator mediator,
        [Description("Todo list id")] Guid todoListId,
        [Description("Todo item label")] string label)
    {
        return await mediator.Send(new CreateTodoItemCommand(label, todoListId));
    }
}
