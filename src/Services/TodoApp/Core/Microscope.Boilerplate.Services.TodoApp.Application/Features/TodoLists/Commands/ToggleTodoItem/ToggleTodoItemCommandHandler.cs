using MediatR;
using Microscope.Boilerplate.Services.TodoApp.Application.Common.Exceptions;
using Microscope.Boilerplate.Services.TodoApp.Application.Policies.CreatedByRequirement;
using Microscope.Boilerplate.Services.TodoApp.Application.Services;
using Microscope.Boilerplate.Services.TodoApp.Domain.Aggregates.TodoListAggregate.Repositories;
using Microscope.SharedKernel;
using Microsoft.AspNetCore.Authorization;

namespace Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Commands.ToggleTodoItem;

public class ToggleTodoItemCommandHandler : IRequestHandler<ToggleTodoItemCommand, bool>
{
    private readonly ITodoListRepository _todoListRepository;
    private readonly IIdentityService _identityService;
    private readonly IAuthorizationService _authorizationService;
    private readonly IUnitOfWork _unitOfWork;

    public ToggleTodoItemCommandHandler(ITodoListRepository todoListRepository, IIdentityService identityService, IAuthorizationService authorizationService, IUnitOfWork unitOfWork)
    {
        _todoListRepository = todoListRepository;
        _identityService = identityService;
        _authorizationService = authorizationService;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<bool> Handle(ToggleTodoItemCommand request, CancellationToken cancellationToken)
    {
        var userId = _identityService.GetUserId();
        var tenantId = _identityService.GetTenantId();

        var todoList = await _todoListRepository.GetByIdAsync(tenantId, request.TodoListId);
        
        var res = await _authorizationService
            .AuthorizeAsync(_identityService.GetClaimsPrincipal(), todoList, new CreatedByRequirement());

        if(!res.Succeeded)
            throw new PoliciesException("Cannot update another user todo list");

        todoList.ToggleItem(request.TodoItemId);

        await _todoListRepository.UpdateAsync(todoList);
        return await _unitOfWork.SaveChangesAndDispatchEventsAsync(cancellationToken);
    }
}