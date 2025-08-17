using MediatR;
using Microscope.Boilerplate.Todo.Domain.TodoListAggregate.Exceptions;
using Microscope.Boilerplate.Todo.Domain.TodoListAggregate.Repositories;
using Microscope.Boilerplate.Todo.Slices.Policies;
using Microscope.Boilerplate.Framework.Application.Exceptions;
using Microscope.Boilerplate.Framework.Application.Services;
using Microscope.Boilerplate.Framework.Domain.DDD;
using Microsoft.AspNetCore.Authorization;

namespace Microscope.Boilerplate.Todo.Slices.Features.UpdateTodoList;

public class UpdateTodoListCommandHandler : IRequestHandler<UpdateTodoListCommand, bool>
{
    private readonly ITodoListRepository _todoListRepository;
    private readonly IIdentityService _identityService;
    private readonly IAuthorizationService _authorizationService;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTodoListCommandHandler(ITodoListRepository todoListRepository, IIdentityService identityService, IAuthorizationService authorizationService, IUnitOfWork unitOfWork)
    {
        _todoListRepository = todoListRepository;
        _identityService = identityService;
        _authorizationService = authorizationService;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<bool> Handle(UpdateTodoListCommand request, CancellationToken cancellationToken)
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

        todoList.Update(request.Name);
        
        await _todoListRepository.UpdateAsync(todoList);
        await _unitOfWork.SaveChangesAndDispatchEventsAsync(cancellationToken);
        
        return true;
    }
}