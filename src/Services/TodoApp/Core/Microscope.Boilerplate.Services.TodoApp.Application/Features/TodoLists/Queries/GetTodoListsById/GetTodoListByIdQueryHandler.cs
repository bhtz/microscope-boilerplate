using AutoMapper;
using MediatR;
using Microscope.Boilerplate.Services.TodoApp.Application.Services;
using Microscope.Boilerplate.Services.TodoApp.Domain.Aggregates.TodoListAggregate.Repositories;

namespace Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Queries.GetTodoListsById;

public class GetTodoListByIdQueryHandler : IRequestHandler<GetTodoListByIdQuery, TodoListByIdQueryResult>
{
    private readonly ITodoListRepository _todoListRepository;
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;
    
    public GetTodoListByIdQueryHandler(ITodoListRepository todoListRepository, IIdentityService identityService, IMapper mapper)
    {
        _todoListRepository = todoListRepository;
        _identityService = identityService;
        _mapper = mapper;
    }
    
    public async Task<TodoListByIdQueryResult> Handle(GetTodoListByIdQuery request, CancellationToken cancellationToken)
    {
        var userId = _identityService.GetUserId();
        var tenantId = _identityService.GetTenantId();

        var todoList = await _todoListRepository.GetByIdAsync(tenantId, request.Id);

        return _mapper.Map<TodoListByIdQueryResult>(todoList);
    }
}

