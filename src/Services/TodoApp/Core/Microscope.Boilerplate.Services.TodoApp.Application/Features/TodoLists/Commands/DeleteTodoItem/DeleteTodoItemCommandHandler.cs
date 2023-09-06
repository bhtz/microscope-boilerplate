using MediatR;
using Microscope.Boilerplate.Services.TodoApp.Application.Common.Exceptions;
using Microscope.Boilerplate.Services.TodoApp.Application.Policies.CreatedByRequirement;
using Microscope.Boilerplate.Services.TodoApp.Application.Services;
using Microscope.Boilerplate.Services.TodoApp.Domain.Aggregates.TodoListAggregate.Exceptions;
using Microscope.Boilerplate.Services.TodoApp.Domain.Aggregates.TodoListAggregate.Repositories;
using Microscope.SharedKernel;
using Microsoft.AspNetCore.Authorization;

namespace Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Commands.DeleteTodoItem;

public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand, bool>
{
    private readonly ITodoListRepository _todoListRepository;
    private readonly IIdentityService _identityService;
    private readonly IAuthorizationService _authorizationService;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTodoItemCommandHandler(ITodoListRepository todoListRepository, IIdentityService identityService, IAuthorizationService authorizationService, IUnitOfWork unitOfWork)
    {
        _todoListRepository = todoListRepository;
        _identityService = identityService;
        _authorizationService = authorizationService;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<bool> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
    {
        var userId = _identityService.GetUserId();
        var tenantId = _identityService.GetTenantId();

        var todoList = await _todoListRepository.GetByIdAsync(tenantId, request.TodoListId);
        
        if (todoList == null) 
            throw new TodoListNotFoundDomainException("Todolist not found");
        
        var res = await _authorizationService
            .AuthorizeAsync(_identityService.GetClaimsPrincipal(), todoList, new CreatedByRequirement());
        
        if(!res.Succeeded)
            throw new PoliciesException("Cannot update another user todo list");

        todoList.RemoveTodoItem(request.TodoItemId);

        await _todoListRepository.UpdateAsync(todoList);
        return await _unitOfWork.SaveChangesAndDispatchEventsAsync(cancellationToken);
    }
}