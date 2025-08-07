using HotChocolate.Authorization;
using MediatR;

namespace Microscope.Boilerplate.Todo.Slices.Features.CreateTodoList;

[QueryType]
public static class CreateTodoListResolver
{
    [AllowAnonymous]
    public static async Task<Guid> CreateTodoList([Service]IMediator mediator, CreateTodoListCommand command)
    {
        return await mediator.Send(command);
    }
}
