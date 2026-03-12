using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using ModelContextProtocol.Server;

namespace Microscope.Boilerplate.Todo.Slices.Features.RemoveTag;

[McpServerToolType]
public static class RemoveTagMcpTool
{
    [Authorize]
    [McpServerTool, Description("Remove a tag from a todo list")]
    public static async Task<bool> RemoveTag(
        IMediator mediator,
        [Description("Todo list id")] Guid todoListId,
        [Description("Tag label")] string label,
        [Description("Tag color")] string color)
    {
        return await mediator.Send(new RemoveTagCommand(label, todoListId, color));
    }
}
