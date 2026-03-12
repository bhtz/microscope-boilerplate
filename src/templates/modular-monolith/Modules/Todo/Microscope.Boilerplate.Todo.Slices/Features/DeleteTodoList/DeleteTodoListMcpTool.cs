using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using ModelContextProtocol.Server;

namespace Microscope.Boilerplate.Todo.Slices.Features.DeleteTodoList;

[McpServerToolType]
public static class DeleteTodoListMcpTool
{
    [Authorize]
    [McpServerTool, Description("Delete a todo list")]
    public static async Task<bool> DeleteTodoList(
        IMediator mediator,
        [Description("Todo list id")] Guid todoListId)
    {
        return await mediator.Send(new DeleteTodoListCommand(todoListId));
    }
}
