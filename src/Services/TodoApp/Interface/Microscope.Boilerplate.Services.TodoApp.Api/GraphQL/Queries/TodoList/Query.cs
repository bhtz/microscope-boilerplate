using HotChocolate.Authorization;
using MediatR;
using Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Queries.GetTodoLists;

namespace Microscope.Boilerplate.Services.TodoApp.Api.GraphQL.Queries;

[Authorize]
public partial class Query
{
    public async Task<IEnumerable<TodoListQueryResult>> GetTodolists([Service]IMediator mediator, string? search)
    {
        var res = await mediator.Send(new GetTodoListQuery());
        return res;
    }
    
    public async Task<TodoListByIdQueryResult> GetTodolistById([Service]IMediator mediator, Guid id)
    {
        var res = await mediator.Send(new GetTodoListByIdQuery(id));
        return res;
    }
}
