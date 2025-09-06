using HotChocolate.Authorization;
using MediatR;
using Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Queries.GetTodoLists;
using Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Queries.GetTodoListsById;

namespace Microscope.Boilerplate.Services.TodoApp.Api.GraphQL.Queries;

[Authorize]
public partial class Query
{
    public async Task<IEnumerable<GetTodoListQueryResult>> GetTodolists([Service]IMediator mediator)
    {
        var res = await mediator.Send(new GetTodoListQuery());
        return res;
    }
    
    public async Task<GetTodoListByIdQueryResult> GetTodolistById([Service]IMediator mediator, Guid id)
    {
        var res = await mediator.Send(new GetTodoListByIdQuery(id));
        return res;
    }
}
