using System.Linq;
using Grpc.Core;
using Microscope.Boilerplate.Todo.Slices.Features.GetTodoLists;
using Microscope.Boilerplate.Todo.Slices.Grpc;
using Microsoft.AspNetCore.Authorization;

namespace Microscope.Boilerplate.Todo.Slices.Services;

public partial class TodoGrpcService
{
    [Authorize]
    public override async Task<GetTodoListsResponse> GetTodoLists(
        GetTodoListsRequest request,
        ServerCallContext context)
    {
        var result = await _mediator.Send(new GetTodoListQuery());

        var response = new GetTodoListsResponse();
        response.TodoLists.AddRange(result.Select(x => new TodoListDto
        {
            Id = x.Id.ToString(),
            Name = x.Name,
            IsCompleted = x.IsCompleted
        }));

        return response;
    }
}
