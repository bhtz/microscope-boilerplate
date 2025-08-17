using HotChocolate.Authorization;
using MediatR;

namespace Microscope.Boilerplate.Todo.Slices.Features.AddTag;

[MutationType]
public static class AddTagResolver
{
    [Authorize]
    public static async Task<bool> AddTag([Service]IMediator mediator, AddTagCommand command)
    {
        return await mediator.Send(command);
    }
}