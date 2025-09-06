using HotChocolate.Authorization;

namespace Microscope.Boilerplate.Todo.Slices.Features.CreateTodoItem;

[MutationType]
public static class CreateTodoItemResolver
{
    [Authorize]
    public static async Task<Guid> CreateTodoItem(IMediator mediator, CreateTodoItemCommand command)
    {
        return await mediator.Send(command);
    }
}