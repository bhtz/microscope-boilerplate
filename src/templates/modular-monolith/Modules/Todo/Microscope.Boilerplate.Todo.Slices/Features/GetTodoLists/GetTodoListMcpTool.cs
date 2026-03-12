using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using ModelContextProtocol.Server;

namespace Microscope.Boilerplate.Todo.Slices.Features.GetTodoLists;

[McpServerToolType]
public static class CreateWorkloadMcpTool
{
    [Authorize]
    [McpServerTool, Description("Get all todolist in the system")]
    public static async Task<IEnumerable<GetTodoListQueryResult>> GetAllTodoLists(IMediator mediator)
    {
        return await mediator.Send(new GetTodoListQuery());
    }
}
