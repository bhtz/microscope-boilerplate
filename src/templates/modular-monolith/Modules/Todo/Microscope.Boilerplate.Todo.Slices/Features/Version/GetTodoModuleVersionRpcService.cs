using Grpc.Core;
using Microscope.Boilerplate.Todo.Slices.Features.Version;
using Microscope.Boilerplate.Todo.Slices.Grpc;

namespace Microscope.Boilerplate.Todo.Slices.Services;

public partial class TodoGrpcService
{
    public override async Task<GetTodoModuleVersionResponse> GetTodoModuleVersion(
        GetTodoModuleVersionRequest request,
        ServerCallContext context)
    {
        var version = await _mediator.Send(new GetTodoVersionQuery());

        return new GetTodoModuleVersionResponse
        {
            Version = version
        };
    }
}
