using Microscope.Boilerplate.Todo.Domain.TodoListAggregate.Repositories;

namespace Microscope.Boilerplate.Todo.Slices.Features.GetTodoLists;

public class GetTodoListQueryHandler(ITodoListRepository todoListRepository, IIdentityService identityService)
    : IRequestHandler<GetTodoListQuery, IEnumerable<GetTodoListQueryResult>>
{
    public async Task<IEnumerable<GetTodoListQueryResult>> Handle(GetTodoListQuery request, CancellationToken cancellationToken)
    {
        var userId = identityService.GetUserId();
        var tenantId = identityService.GetTenantId();

        var todoLists = await todoListRepository.GetCreatedByAsync(tenantId, userId);

        return todoLists
            .Select(x => new GetTodoListQueryResult(x.Id, x.Name, x.IsCompleted))
            .ToList();
    }
}

