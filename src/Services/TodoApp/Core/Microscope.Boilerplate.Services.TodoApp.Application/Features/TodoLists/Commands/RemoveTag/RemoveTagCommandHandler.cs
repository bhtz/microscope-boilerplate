using MediatR;
using Microscope.Boilerplate.Services.TodoApp.Application.Common.Exceptions;
using Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Commands.AddTag;
using Microscope.Boilerplate.Services.TodoApp.Application.Policies.CreatedByRequirement;
using Microscope.Boilerplate.Services.TodoApp.Application.Services;
using Microscope.Boilerplate.Services.TodoApp.Domain.Aggregates.TodoListAggregate.Repositories;
using Microscope.Boilerplate.Services.TodoApp.Domain.Aggregates.TodoListAggregate.ValueObjects;
using Microscope.SharedKernel;
using Microsoft.AspNetCore.Authorization;

namespace Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Commands.RemoveTag;

public class RemoveTagCommandHandler : IRequestHandler<RemoveTagCommand, bool>
{
    private readonly ITodoListRepository _todoListRepository;
    private readonly IIdentityService _identityService;
    private readonly IAuthorizationService _authorizationService;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveTagCommandHandler(ITodoListRepository todoListRepository, IIdentityService identityService, IAuthorizationService authorizationService, IUnitOfWork unitOfWork)
    {
        _todoListRepository = todoListRepository;
        _identityService = identityService;
        _authorizationService = authorizationService;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<bool> Handle(RemoveTagCommand request, CancellationToken cancellationToken)
    {
        var userId = _identityService.GetUserId();
        var tenantId = _identityService.GetTenantId();

        var todoList = await _todoListRepository.GetByIdAsync(tenantId, request.TodoListId);
        
        var res = await _authorizationService
            .AuthorizeAsync(_identityService.GetClaimsPrincipal(), todoList, new CreatedByRequirement());

        if(!res.Succeeded)
            throw new PoliciesException("Cannot update another user todo list");

        var tag = new Tag(request.Label, request.Color);
        todoList.RemoveTag(tag);

        await _todoListRepository.UpdateAsync(todoList);
        return await _unitOfWork.SaveChangesAndDispatchEventsAsync(cancellationToken);
    }
}