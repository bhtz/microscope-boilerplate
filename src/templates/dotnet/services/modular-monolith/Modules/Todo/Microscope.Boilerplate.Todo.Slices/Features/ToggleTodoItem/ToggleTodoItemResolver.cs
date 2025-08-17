using HotChocolate.Authorization;
using MediatR;

namespace Microscope.Boilerplate.Todo.Slices.Features.ToggleTodoItem;

[MutationType]
public static class ToggleTodoItemResolver
{
    [Authorize]
    public static async Task<bool> ToggleTodoItem([Service]IMediator mediator, ToggleTodoItemCommand command)
    {
        return await mediator.Send(command);
    }
}