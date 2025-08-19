using System;
using Grpc.Core;
using Microscope.Boilerplate.Todo.Slices.Features.DeleteTodoItem;
using Microscope.Boilerplate.Todo.Slices.Grpc;

namespace Microscope.Boilerplate.Todo.Slices.Services;

public partial class TodoGrpcService
{
    public override async Task<DeleteTodoItemResponse> DeleteTodoItem(
        DeleteTodoItemRequest request,
        ServerCallContext context)
    {
        if (!Guid.TryParse(request.TodoListId, out var todoListId))
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid TodoListId format"));
        }

        if (!Guid.TryParse(request.TodoItemId, out var todoItemId))
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid TodoItemId format"));
        }

        var result = await _mediator.Send(new DeleteTodoItemCommand(todoListId, todoItemId));

        return new DeleteTodoItemResponse
        {
            Success = result
        };
    }
}
