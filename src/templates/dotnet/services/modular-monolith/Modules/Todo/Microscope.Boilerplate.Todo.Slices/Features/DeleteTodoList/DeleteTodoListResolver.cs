using HotChocolate.Authorization;
using MediatR;

namespace Microscope.Boilerplate.Todo.Slices.Features.DeleteTodoList;

[MutationType]
public static class DeleteTodoListResolver
{
    public static async Task<bool> DeleteTodoList([Service]IMediator mediator, DeleteTodoListCommand command)
    {
        return await mediator.Send(command);
    }
}