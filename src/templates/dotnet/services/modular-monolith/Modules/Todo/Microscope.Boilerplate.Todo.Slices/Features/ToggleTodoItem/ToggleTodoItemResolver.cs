using HotChocolate.Authorization;
using MediatR;
using Microscope.Boilerplate.Todo.Slices.Features.RemoveTag;

namespace Microscope.Boilerplate.Todo.Slices.Features.ToggleTodoItem;

[MutationType]
public static class RemoveTagResolver
{
    [AllowAnonymous]
    public static async Task<bool> RemoveTag([Service]IMediator mediator, RemoveTagCommand command)
    {
        return await mediator.Send(command);
    }
}