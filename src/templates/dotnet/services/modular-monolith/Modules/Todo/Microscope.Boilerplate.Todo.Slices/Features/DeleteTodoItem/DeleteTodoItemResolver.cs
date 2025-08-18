using HotChocolate.Authorization;
using MediatR;

namespace Microscope.Boilerplate.Todo.Slices.Features.DeleteTodoItem;

[MutationType]
public static class DeleteTodoItemResolver
{
    [Authorize]
    public static async Task<bool> DeleteTodoItem([Service]IMediator mediator, DeleteTodoItemCommand command)
    {
        return await mediator.Send(command);
    }
}