using HotChocolate.Authorization;

namespace Microscope.Boilerplate.Todo.Slices.Features.GetTodoListsById;

[QueryType]
public static class GetTodoListByIdResolver
{
    [Authorize]
    public static async Task<GetTodoListByIdQueryResult> GetTodoListById(IMediator mediator, GetTodoListByIdQuery query)
    {
        return await mediator.Send(query);
    }
}