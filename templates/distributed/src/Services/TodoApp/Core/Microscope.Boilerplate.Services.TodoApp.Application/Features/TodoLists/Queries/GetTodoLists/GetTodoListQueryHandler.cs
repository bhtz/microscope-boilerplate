using AutoMapper;
using MediatR;
using Microscope.Boilerplate.Services.TodoApp.Application.Services;
using Microscope.Boilerplate.Services.TodoApp.Domain.Aggregates.TodoListAggregate.Repositories;

namespace Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Queries.GetTodoLists;

public class GetTodoListQueryHandler : IRequestHandler<GetTodoListQuery, IEnumerable<GetTodoListQueryResult>>
{
    private readonly ITodoListRepository _todoListRepository;
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;
    
    public GetTodoListQueryHandler(ITodoListRepository todoListRepository, IIdentityService identityService, IMapper mapper)
    {
        _todoListRepository = todoListRepository;
        _identityService = identityService;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<GetTodoListQueryResult>> Handle(GetTodoListQuery request, CancellationToken cancellationToken)
    {
        var userId = _identityService.GetUserId();
        var tenantId = _identityService.GetTenantId();

        var todoLists = await _todoListRepository.GetCreatedByAsync(tenantId, userId);

        return _mapper.Map<IEnumerable<GetTodoListQueryResult>>(todoLists);
    }
}

