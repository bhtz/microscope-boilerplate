using HotChocolate.Authorization;
using MediatR;

namespace Microscope.Boilerplate.Todo.Slices.Features.RemoveTag;

[MutationType]
public static class RemoveTagResolver
{
    public static async Task<bool> RemoveTag([Service]IMediator mediator, RemoveTagCommand command)
    {
        return await mediator.Send(command);
    }
}