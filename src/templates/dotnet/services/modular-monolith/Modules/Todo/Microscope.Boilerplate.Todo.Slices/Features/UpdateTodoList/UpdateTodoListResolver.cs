using HotChocolate.Authorization;
using MediatR;

namespace Microscope.Boilerplate.Todo.Slices.Features.UpdateTodoList;

[MutationType]
public static class UpdateTodoListResolver
{
    [Authorize]
    public static async Task<bool> UpdateTodoList([Service]IMediator mediator, UpdateTodoListCommand command)
    {
        return await mediator.Send(command);
    }
}