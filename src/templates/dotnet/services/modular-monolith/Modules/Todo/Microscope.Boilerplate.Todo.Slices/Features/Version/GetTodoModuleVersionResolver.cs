using HotChocolate.Authorization;

namespace Microscope.Boilerplate.Todo.Slices.Features.Version;

[QueryType]
public static class GetTodoModuleVersionResolver
{
    [AllowAnonymous]
    public static async Task<string> GetTodoModuleVersion([Service]IMediator mediator)
    {
        return await mediator.Send(new GetTodoVersionQuery());
    }
}
