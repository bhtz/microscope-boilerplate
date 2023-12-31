using MediatR;
using Microscope.Boilerplate.Services.TodoApp.Application.Common.Exceptions;
using Microscope.Boilerplate.Services.TodoApp.Application.Services;
using Microscope.Boilerplate.Services.TodoApp.Domain.Aggregates.TodoListAggregate;
using Microscope.Boilerplate.Services.TodoApp.Domain.Aggregates.TodoListAggregate.Exceptions;
using Microscope.Boilerplate.Services.TodoApp.Domain.Aggregates.TodoListAggregate.Repositories;
using Microscope.SharedKernel;
using Microsoft.AspNetCore.Authorization;

namespace Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Commands.CreateTodoList;

public class CreateTodoListCommandHandler : IRequestHandler<CreateTodoListCommand, Guid>
{
    private readonly ITodoListRepository _todoListRepository;
    private readonly IIdentityService _identityService;
    private readonly IAuthorizationService _authorizationService;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTodoListCommandHandler(ITodoListRepository todoListRepository, IIdentityService identityService, IAuthorizationService authorizationService, IUnitOfWork unitOfWork)
    {
        _todoListRepository = todoListRepository;
        _identityService = identityService;
        _authorizationService = authorizationService;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Guid> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
    {
        var userId = _identityService.GetUserId();
        var userMail = _identityService.GetUserMail();
        var tenantId = _identityService.GetTenantId();

        var todoList = TodoList.Create(tenantId, Guid.NewGuid(), userId, userMail, request.Name);
        
        if (todoList == null) 
            throw new TodoListNotFoundDomainException("Todolist not found");
        
        await _todoListRepository.AddAsync(todoList);
        await _unitOfWork.SaveChangesAndDispatchEventsAsync(cancellationToken);

        return todoList.Id;
    }
}