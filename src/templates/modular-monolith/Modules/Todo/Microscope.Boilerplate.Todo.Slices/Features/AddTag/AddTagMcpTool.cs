using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using ModelContextProtocol.Server;

namespace Microscope.Boilerplate.Todo.Slices.Features.AddTag;

[McpServerToolType]
public static class AddTagMcpTool
{
    [Authorize]
    [McpServerTool, Description("Add a tag to a todo list")]
    public static async Task<bool> AddTag(
        IMediator mediator,
        [Description("Todo list id")] Guid todoListId,
        [Description("Tag label")] string label,
        [Description("Tag color")] string color)
    {
        return await mediator.Send(new AddTagCommand(label, todoListId, color));
    }
}
