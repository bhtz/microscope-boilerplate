using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microscope.Boilerplate.Todo.Slices.Grpc;

namespace Microscope.Boilerplate.Todo.Slices.Services;

public partial class TodoGrpcService : TodoService.TodoServiceBase
{
    private readonly IMediator _mediator;

    public TodoGrpcService(IMediator mediator)
    {
        _mediator = mediator;
    }
}
