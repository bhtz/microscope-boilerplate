using Microscope.Boilerplate.Services.TodoApp.Application.Policies.CreatedByRequirement;
using Microscope.Boilerplate.Services.TodoApp.Application.Services;
using Microscope.Boilerplate.Services.TodoApp.Domain.Aggregates.TodoListAggregate;
using Microsoft.AspNetCore.Authorization;

namespace Microscope.Boilerplate.Services.TodoApp.Application.Features.TodoLists.Policies;

public class TodoListCreatedByRequirementHandler : AuthorizationHandler<CreatedByRequirement, TodoList>
{
    private readonly IIdentityService _identityService;

    public TodoListCreatedByRequirementHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CreatedByRequirement requirement, TodoList resource)
    {
        var userId = _identityService.GetUserId();
        
        if (resource.CreatedBy == userId)
        {
            context.Succeed(requirement);    
        }
        else
        {
            context.Fail();   
        }

        return Task.CompletedTask;
    }
}