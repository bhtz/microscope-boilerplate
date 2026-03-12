using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using ModelContextProtocol.Server;

namespace Microscope.Boilerplate.Todo.Slices.Features.GetTodoListsById;

[McpServerToolType]
public static class GetTodoListByIdMcpTool
{
    [Authorize]
    [McpServerTool, Description("Get a todo list by id")]
    public static async Task<GetTodoListByIdQueryResult> GetTodoListById(
        IMediator mediator,
        [Description("Todo list id")] Guid id)
    {
        return await mediator.Send(new GetTodoListByIdQuery(id));
    }
}
