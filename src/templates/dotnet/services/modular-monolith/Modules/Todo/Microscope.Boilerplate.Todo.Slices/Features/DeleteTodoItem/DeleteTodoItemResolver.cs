using HotChocolate.Authorization;
using MediatR;
using Microscope.Boilerplate.Todo.Slices.Features.CreateTodoItem;

namespace Microscope.Boilerplate.Todo.Slices.Features.DeleteTodoItem;

[MutationType]
public static class DeleteTodoItemResolver
{
    public static async Task<bool> DeleteTodoItem([Service]IMediator mediator, DeleteTodoItemCommand command)
    {
        return await mediator.Send(command);
    }
}