using HotChocolate.Authorization;

namespace Microscope.Boilerplate.Todo.Slices.Features.DeleteTodoList;

[MutationType]
public static class DeleteTodoListResolver
{
    [Authorize]
    public static async Task<bool> DeleteTodoList(IMediator mediator, DeleteTodoListCommand command)
    {
        return await mediator.Send(command);
    }
}