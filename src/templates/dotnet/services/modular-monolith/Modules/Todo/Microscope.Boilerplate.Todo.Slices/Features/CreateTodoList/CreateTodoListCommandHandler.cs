using MediatR;
using Microscope.Boilerplate.Todo.Domain.TodoListAggregate;
using Microscope.Boilerplate.Todo.Domain.TodoListAggregate.Exceptions;
using Microscope.Boilerplate.Todo.Domain.TodoListAggregate.Repositories;
using Microscope.Boilerplate.Framework.Application.Services;
using Microscope.Boilerplate.Framework.Domain.DDD;

namespace Microscope.Boilerplate.Todo.Slices.Features.CreateTodoList;

public class CreateTodoListCommandHandler(
    IUnitOfWork unitOfWork,
    ITodoListRepository todoListRepository,
    IIdentityService identityService) : IRequestHandler<CreateTodoListCommand, Guid>
{
    public async Task<Guid> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
    {
        var userId = identityService.GetUserId();
        var userMail = identityService.GetUserMail();
        var tenantId =  identityService.GetTenantId();

        var todoList = TodoList.Create(tenantId, Guid.NewGuid(), userId, userMail, request.Name);
        
        if (todoList == null)
            throw new TodoListNotFoundDomainException("Todolist not found");
        
        await todoListRepository.AddAsync(todoList);
        await unitOfWork.SaveChangesAndDispatchEventsAsync(cancellationToken);

        return todoList.Id;
    }
}