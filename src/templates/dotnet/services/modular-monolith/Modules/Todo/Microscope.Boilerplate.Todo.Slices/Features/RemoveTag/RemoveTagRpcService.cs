using System;
using Grpc.Core;
using Microscope.Boilerplate.Todo.Slices.Features.RemoveTag;
using Microscope.Boilerplate.Todo.Slices.Grpc;
using Microsoft.AspNetCore.Authorization;

namespace Microscope.Boilerplate.Todo.Slices.Services;

public partial class TodoGrpcService
{
    [Authorize]
    public override async Task<RemoveTagResponse> RemoveTag(
        RemoveTagRequest request,
        ServerCallContext context)
    {
        if (!Guid.TryParse(request.TodoListId, out var todoListId))
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid TodoListId format"));
        }

        var result = await _mediator.Send(new RemoveTagCommand(
            request.Label,
            todoListId,
            request.Color));

        return new RemoveTagResponse
        {
            Success = result
        };
    }
}
