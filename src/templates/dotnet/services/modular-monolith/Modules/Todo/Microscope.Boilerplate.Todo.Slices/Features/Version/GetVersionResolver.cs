using MediatR;

namespace Microscope.Boilerplate.Todo.Slices.Features.Version;

[QueryType]
public static class GetVersionResolver
{
    public static async Task<string> GetApiVersion([Service]IMediator mediator)
    {
        return await mediator.Send(new GetVersionQuery());
    }
}
