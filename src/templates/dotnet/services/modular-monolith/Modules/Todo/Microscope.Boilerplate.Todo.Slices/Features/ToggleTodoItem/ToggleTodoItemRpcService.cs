using System;
using Grpc.Core;
using Microscope.Boilerplate.Todo.Slices.Features.ToggleTodoItem;
using Microscope.Boilerplate.Todo.Slices.Grpc;
using Microsoft.AspNetCore.Authorization;

namespace Microscope.Boilerplate.Todo.Slices.Services;

public partial class TodoGrpcService
{
    [Authorize]
    public override async Task<ToggleTodoItemResponse> ToggleTodoItem(
        ToggleTodoItemRequest request,
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

        var result = await _mediator.Send(new ToggleTodoItemCommand(todoListId, todoItemId));

        return new ToggleTodoItemResponse
        {
            Success = result
        };
    }
}
