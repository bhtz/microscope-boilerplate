using HotChocolate.Authorization;

namespace Microscope.Boilerplate.Todo.Slices.Features.CreateTodoList;

[MutationType]
public static class CreateTodoListResolver
{
    // [Authorize(JwtBearerDefaults.AuthenticationScheme)]
    // [Authorize(ApiKeyDefaults.AuthenticationScheme)]
    [Authorize]
    public static async Task<Guid> CreateTodoList(IMediator mediator, CreateTodoListCommand command)
    {
        return await mediator.Send(command);
    }
}
