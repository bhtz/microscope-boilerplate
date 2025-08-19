using Grpc.Core;
using Microscope.Boilerplate.Todo.Slices.Features.DeleteTodoList;
using Microscope.Boilerplate.Todo.Slices.Grpc;
using Microsoft.AspNetCore.Authorization;

namespace Microscope.Boilerplate.Todo.Slices.Services;

public partial class TodoGrpcService
{
    [Authorize]
    public override async Task<DeleteTodoListResponse> DeleteTodoList(
        DeleteTodoListRequest request,
        ServerCallContext context)
    {
        if (!Guid.TryParse(request.Id, out var id))
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid ID format"));
        }

        var result = await _mediator.Send(new DeleteTodoListCommand(id));

        return new DeleteTodoListResponse
        {
            Success = result
        };
    }
}
