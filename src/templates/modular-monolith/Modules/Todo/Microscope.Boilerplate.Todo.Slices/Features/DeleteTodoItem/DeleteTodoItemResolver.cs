using HotChocolate.Authorization;

namespace Microscope.Boilerplate.Todo.Slices.Features.DeleteTodoItem;

[MutationType]
public static class DeleteTodoItemResolver
{
    [Authorize]
    public static async Task<bool> DeleteTodoItem(IMediator mediator, DeleteTodoItemCommand command)
    {
        return await mediator.Send(command);
    }
}