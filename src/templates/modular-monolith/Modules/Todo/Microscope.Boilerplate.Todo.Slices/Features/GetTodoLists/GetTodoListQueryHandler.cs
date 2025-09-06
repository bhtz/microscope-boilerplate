using Microscope.Boilerplate.Todo.Domain.TodoListAggregate.Repositories;

namespace Microscope.Boilerplate.Todo.Slices.Features.GetTodoLists;

public class GetTodoListQueryHandler : IRequestHandler<GetTodoListQuery, IEnumerable<GetTodoListQueryResult>>
{
    private readonly ITodoListRepository _todoListRepository;
    private readonly IIdentityService _identityService;
    
    public GetTodoListQueryHandler(ITodoListRepository todoListRepository, IIdentityService identityService)
    {
        _todoListRepository = todoListRepository;
        _identityService = identityService;
    }
    
    public async Task<IEnumerable<GetTodoListQueryResult>> Handle(GetTodoListQuery request, CancellationToken cancellationToken)
    {
        var userId = _identityService.GetUserId();
        var tenantId = _identityService.GetTenantId();

        var todoLists = await _todoListRepository.GetCreatedByAsync(tenantId, userId);

        return todoLists
            .Select(x => new GetTodoListQueryResult(x.CreatedBy, x.Name, x.IsCompleted))
            .ToList();
    }
}

