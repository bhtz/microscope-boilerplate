using HotChocolate.Authorization;
using MediatR;

namespace Microscope.Boilerplate.Todo.Slices.Features.GetTodoLists;

[QueryType]
public static class GetTodoListResolver
{
    public static async Task<IEnumerable<GetTodoListQueryResult>> GetTodoLists([Service]IMediator mediator)
    {
        return await mediator.Send(new GetTodoListQuery());
    }
}