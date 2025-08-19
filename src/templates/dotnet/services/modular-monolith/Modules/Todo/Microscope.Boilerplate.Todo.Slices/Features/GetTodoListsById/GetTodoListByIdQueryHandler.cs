using Microscope.Boilerplate.Todo.Domain.TodoListAggregate.Exceptions;
using Microscope.Boilerplate.Todo.Domain.TodoListAggregate.Repositories;

namespace Microscope.Boilerplate.Todo.Slices.Features.GetTodoListsById;

public class GetTodoListByIdQueryHandler : IRequestHandler<GetTodoListByIdQuery, GetTodoListByIdQueryResult>
{
    private readonly ITodoListRepository _todoListRepository;
    private readonly IIdentityService _identityService;
    
    public GetTodoListByIdQueryHandler(ITodoListRepository todoListRepository, IIdentityService identityService)
    {
        _todoListRepository = todoListRepository;
        _identityService = identityService;
    }
    
    public async Task<GetTodoListByIdQueryResult> Handle(GetTodoListByIdQuery request, CancellationToken cancellationToken)
    {
        var userId = _identityService.GetUserId();
        var tenantId = _identityService.GetTenantId();

        var todoList = await _todoListRepository.GetByIdAsync(tenantId, request.Id)
            ?? throw new TodoListNotFoundDomainException("Todolist not found");
        
        return new GetTodoListByIdQueryResult()
        {
            Id = todoList.Id,
            IsCompleted = todoList.IsCompleted,
            Name = todoList.Name,
            Tags = todoList.Tags.Select(x => new TagResult(x.Label, x.Color)),
            TodoItems = todoList.TodoItems.Select(x => new TodoItemResult(x.Id, x.Label, x.IsCompleted))
        };
    }
}

