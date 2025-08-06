using HotChocolate.Authorization;
using MediatR;

namespace Microscope.Boilerplate.Todo.Slices.Features.Version;

[QueryType]
public static class GetVersionResolver
{
    [AllowAnonymous]
    public static async Task<string> GetApiVersion([Service]IMediator mediator)
    {
        return await mediator.Send(new GetVersionQuery());
    }
}
