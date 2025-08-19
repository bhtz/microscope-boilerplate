using System;
using Grpc.Core;
using Microscope.Boilerplate.Todo.Slices.Features.ToggleTodoItem;
using Microscope.Boilerplate.Todo.Slices.Grpc;

namespace Microscope.Boilerplate.Todo.Slices.Services;

public partial class TodoGrpcService
{
    public override async Task<ToggleTodoItemResponse> ToggleTodoItem(
        ToggleTodoItemRequest request,
        ServerCallContext context)
    {
        if (!Guid.TryParse(request.Id, out var id))
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid ID format"));
        }

        var result = await _mediator.Send(new ToggleTodoItemCommand(id, Guid.NewGuid()));

        return new ToggleTodoItemResponse
        {
            Success = result
        };
    }
}
