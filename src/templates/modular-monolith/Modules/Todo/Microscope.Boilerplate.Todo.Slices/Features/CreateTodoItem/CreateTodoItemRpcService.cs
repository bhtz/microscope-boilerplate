using Grpc.Core;
using Microscope.Boilerplate.Todo.Slices.Features.CreateTodoItem;
using Microscope.Boilerplate.Todo.Slices.Grpc;
using Microsoft.AspNetCore.Authorization;

namespace Microscope.Boilerplate.Todo.Slices.Services;

public partial class TodoGrpcService
{
    [Authorize]
    public override async Task<CreateTodoItemResponse> CreateTodoItem(
        CreateTodoItemRequest request,
        ServerCallContext context)
    {
        if (!Guid.TryParse(request.TodoListId, out var todoListId))
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid TodoListId format"));
        }

        var result = await _mediator.Send(new CreateTodoItemCommand(
            request.Title,
            todoListId));

        return new CreateTodoItemResponse
        {
            Id = result.ToString()
        };
    }
}
