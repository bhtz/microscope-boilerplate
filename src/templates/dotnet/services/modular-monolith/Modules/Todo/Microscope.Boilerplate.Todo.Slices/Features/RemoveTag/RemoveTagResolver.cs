using HotChocolate.Authorization;

namespace Microscope.Boilerplate.Todo.Slices.Features.RemoveTag;

[MutationType]
public static class RemoveTagResolver
{
    [Authorize]
    public static async Task<bool> RemoveTag(IMediator mediator, RemoveTagCommand command)
    {
        return await mediator.Send(command);
    }
}