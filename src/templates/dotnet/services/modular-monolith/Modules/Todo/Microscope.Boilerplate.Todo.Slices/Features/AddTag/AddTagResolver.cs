using HotChocolate.Authorization;

namespace Microscope.Boilerplate.Todo.Slices.Features.AddTag;

[MutationType]
public static class AddTagResolver
{
    [Authorize]
    public static async Task<bool> AddTag(IMediator mediator, AddTagCommand command)
    {
        return await mediator.Send(command);
    }
}