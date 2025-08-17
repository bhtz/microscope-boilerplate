using HotChocolate.Authorization;
using MediatR;

namespace Microscope.Boilerplate.Todo.Slices.Features.CreateTodoList;

[MutationType]
public static class CreateTodoListResolver
{
    // [Authorize(JwtBearerDefaults.AuthenticationScheme)]
    // [Authorize(ApiKeyDefaults.AuthenticationScheme)]
    [Authorize]
    public static async Task<Guid> CreateTodoList([Service]IMediator mediator, CreateTodoListCommand command)
    {
        return await mediator.Send(command);
    }
}
