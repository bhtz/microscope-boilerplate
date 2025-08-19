using Grpc.Core;
using Microscope.Boilerplate.Todo.Slices.Features.CreateTodoList;
using Microscope.Boilerplate.Todo.Slices.Grpc;

namespace Microscope.Boilerplate.Todo.Slices.Services;

public partial class TodoGrpcService
{
    public override async Task<CreateTodoListResponse> CreateTodoList(
        CreateTodoListRequest request,
        ServerCallContext context)
    {
        var result = await _mediator.Send(new CreateTodoListCommand(request.Name));

        return new CreateTodoListResponse
        {
            Id = result.ToString()
        };
    }
}
