using Grpc.Core;
using Microscope.Boilerplate.Todo.Slices.Features.UpdateTodoList;
using Microscope.Boilerplate.Todo.Slices.Grpc;
using Microsoft.AspNetCore.Authorization;

namespace Microscope.Boilerplate.Todo.Slices.Services;

public partial class TodoGrpcService
{
    [Authorize]
    public override async Task<UpdateTodoListResponse> UpdateTodoList(
        UpdateTodoListRequest request,
        ServerCallContext context)
    {
        if (!Guid.TryParse(request.Id, out var id))
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid ID format"));
        }

        var result = await _mediator.Send(new UpdateTodoListCommand(request.Name, id));

        return new UpdateTodoListResponse
        {
            Success = result
        };
    }
}
