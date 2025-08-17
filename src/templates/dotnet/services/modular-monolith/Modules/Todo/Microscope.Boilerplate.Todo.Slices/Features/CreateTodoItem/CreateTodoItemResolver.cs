using HotChocolate.Authorization;
using MediatR;

namespace Microscope.Boilerplate.Todo.Slices.Features.CreateTodoItem;

[MutationType]
public static class CreateTodoItemResolver
{
    [Authorize]
    public static async Task<Guid> CreateTodoItem([Service]IMediator mediator, CreateTodoItemCommand command)
    {
        return await mediator.Send(command);
    }
}