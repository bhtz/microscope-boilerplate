using MediatR;
using Microscope.Boilerplate.Todo.Domain.TodoListAggregate.Exceptions;
using Microscope.Boilerplate.Todo.Domain.TodoListAggregate.Repositories;
using Microscope.Boilerplate.Todo.Slices.Policies;
using Microscope.Framework.Application.Exceptions;
using Microscope.Framework.Application.Services;
using Microscope.Framework.Domain.DDD;
using Microsoft.AspNetCore.Authorization;
using Tag = Microscope.Boilerplate.Todo.Domain.TodoListAggregate.ValueObjects.Tag;

namespace Microscope.Boilerplate.Todo.Slices.Features.AddTag;

public class AddTagCommandHandler : IRequestHandler<AddTagCommand, bool>
{
    private readonly ITodoListRepository _todoListRepository;
    private readonly IIdentityService _identityService;
    private readonly IAuthorizationService _authorizationService;
    private readonly IUnitOfWork _unitOfWork;

    public AddTagCommandHandler(ITodoListRepository todoListRepository, IIdentityService identityService, IAuthorizationService authorizationService, IUnitOfWork unitOfWork)
    {
        _todoListRepository = todoListRepository;
        _identityService = identityService;
        _authorizationService = authorizationService;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<bool> Handle(AddTagCommand request, CancellationToken cancellationToken)
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
        
        var tag = new Tag(request.Label, request.Color);
        todoList.AddTag(tag);

        await _todoListRepository.UpdateAsync(todoList);
        await _unitOfWork.SaveChangesAndDispatchEventsAsync(cancellationToken);
        
        return true;
    }
}
