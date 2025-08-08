using HotChocolate.Authorization;
using MediatR;
using Microscope.Boilerplate.Todo.Slices.Features.AddTag;

namespace Microscope.Boilerplate.Todo.Slices.Features.CreateTodoItem;

[MutationType]
public static class CreateTodoItemResolver
{
    public static async Task<Guid> CreateTodoItem([Service]IMediator mediator, CreateTodoItemCommand command)
    {
        return await mediator.Send(command);
    }
}