using System;
using Grpc.Core;
using Microscope.Boilerplate.Todo.Slices.Features.GetTodoListsById;
using Microscope.Boilerplate.Todo.Slices.Grpc;

namespace Microscope.Boilerplate.Todo.Slices.Services;

public partial class TodoGrpcService
{
    public override async Task<GetTodoListByIdResponse> GetTodoListById(
        GetTodoListByIdRequest request,
        ServerCallContext context)
    {
        if (!Guid.TryParse(request.Id, out var id))
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid ID format"));
        }

        var result = await _mediator.Send(new GetTodoListByIdQuery(id));

        return new GetTodoListByIdResponse
        {
            TodoList = new TodoListDto
            {
                Id = result.Id.ToString(),
                Name = result.Name,
                IsCompleted = result.IsCompleted
            }
        };
    }
}
