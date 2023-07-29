using HotChocolate.Authorization;
using MediatR;
using Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Queries.GetTodoLists;

namespace Microscope.Boilerplate.Services.TodoApp.Api.GraphQL.Queries;

[Authorize]
public partial class Query
{
    public async Task<IEnumerable<TodoListQueryResult>> GetTodolists([Service]IMediator mediator, GetTodoListQuery query)
    {
        var res = await mediator.Send(query);
        return res;
    }
}
