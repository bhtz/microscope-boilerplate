using Microscope.Boilerplate.Todo.Domain.TodoListAggregate;
using Microsoft.AspNetCore.Authorization;

namespace Microscope.Boilerplate.Todo.Slices.Policies;

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